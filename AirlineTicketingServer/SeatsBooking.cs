using ClientCommunication;
using Communication;
using FlightsOptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Server {
public static class SeatsBooking {

    public static Either<Success, InputError> deleteBookedSeat(string surname, string pnr) {
        using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
        using(
        var command = new SqlCommand(
            "[Flights].[UnbookSeat]", connection
        )) {
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@Surname", surname == null ? DBNull.Value : (object) surname);
        command.Parameters.AddWithValue("@PNR", pnr);

        connection.Open();
        try{ command.ExecuteNonQuery(); }
        catch(SqlException e) {
            if(e.State == 2) {
                return Either<Success, InputError>.Failure(
                    new InputError("Данная бронь не найден")
                );
            }
            else if(e.State == 5) {
                return Either<Success, InputError>.Failure(
                    new InputError("Данная бронь не найдена или не может быть отменена")
                );
            }
            else if(e.State == 10) {
                return Either<Success, InputError>.Failure(
                    new InputError("Бронь на данный полёт уже нельзя отменить")
                );
            }
            else if(e.State == 20) {
                return Either<Success, InputError>.Failure(
                    new InputError("Внутренняя ошибка")
                );
            }
            else throw e;
        }

        return Either<Success, InputError>.Success(new Success());
        }}
    }

    private sealed class RawPassangerBookedFlight {
        public BookedFlight bookedFlight;
        public Flight availableFlight;
        public Passanger passanger;

        public int passangerId;
        public byte[] documentBin;
        public byte[] optionsBin;
        public byte[] selectedOptionsBin;
        public bool cancelled;
        public short seatIndex;
        public string pnr;
        public byte seatClass;

        public RawPassangerBookedFlight() {
            bookedFlight = new BookedFlight();
            availableFlight = new Flight();
            bookedFlight.availableFlight = availableFlight;
            passanger = new Passanger();
        }
    }

    public static Either<List<PassangerBookedFlight>, InputError> getBookedFlight(
        SqlConnectionView cv,
        OperatorCommunication.PassangerSearchParams ps
    ) {
        using(cv) {
        using(var command = new SqlCommand(
            @"
            select
                [cbf].[Id] as [BookedFlightId],
                [af].[Id] as [AvailableFlightId],
                concat([fi].[AirlineDesignator], ' ', cast([fi].[Number] as char(4))) as [FlightName],
                [ap].[Name] as [AirplaneName],
                dateadd(minute, [af].[DepartureTimeMinutes], cast([af].[DepartureDate] as datetime)) as [DepDatetime],
                [fi].[ArrivalOffsetMinutes] as [ArrivalOffsetMinutes],
                [fi].[Options] as [Options],
                [fi].[FromCity],
                [fi].[ToCity],
                [cbf].[PassangersCount],
                [cbf].[BookedDatetime],

                [pr].[Id], [pr].[Archived], [pr].[Name], [pr].[Surname],
                [pr].[MiddleName], [pr].[Birthday], [pr].[Document],

                [afs].[SelectedOptions], cast(case when [afs].[CanceledIndex] != 0 then 1 else 0 end as bit),
                [afs].[SeatIndex], [afs].[PNR], [aps].[Class]
            from (
                select *
                from [Flights].[AvailableFlightsSeats] as [afs]
                where (upper([afs].[PNR]) = upper(@PNR) or @PNR is null)
            ) as [afs]

            inner join [Customers].[Passanger] as [pr]
            on [afs].[Passanger] = [pr].[Id]
                and (@Name is null       or upper([pr].[Name]) = upper(@Name))
                and (@Surname is null    or upper([pr].[Surname]) = upper(@Surname))
                and (@MiddleName is null or upper([pr].[MiddleName]) = upper(@MiddleName))

            inner join [Flights].[AvailableFlights] as [af]
            on [af].[Id] = [afs].[AvailableFlight]

            inner join [Flights].[FlightInfo] as [fi]
            on [af].[FlightInfo]  = [fi].[Id]

            inner join [Flights].[Airplanes] as [ap]
            on [fi].[Airplane] = [ap].[Id]

            inner join [Flights].[AirplanesSeats] as [aps]
            on [ap].[Id] = [aps].[Airplane]
                and [aps].[SeatIndex] = [afs].[SeatIndex]

            left join [Customers].[CustomersBookedFlights] as [cbf]
            on [afs].[CustomerBookedFlightId] = [cbf].[Id]",
            cv.connection
        )) {
        command.CommandType = CommandType.Text;
        command.Parameters.AddWithValue("@PNR", ps.pnr.wrapDBNull());
        command.Parameters.AddWithValue("@Name", ps.name.wrapDBNull());
        command.Parameters.AddWithValue("@Surname", ps.surname.wrapDBNull());
        command.Parameters.AddWithValue("@MiddleName", ps.middleName.wrapDBNull());

        //var extractor = new DatabaseSeatsExtraction();
        var rawFlights = new List<RawPassangerBookedFlight>();

        cv.Open();
        using(
        var reader = command.ExecuteReader()) {
        while(reader.Read()) {
            var it = new RawPassangerBookedFlight();

            it.bookedFlight.bookedFlightId = (int?) (reader[0] is DBNull ? null : reader[0]);
            it.availableFlight.id = (int) reader[1];
            it.availableFlight.flightName = (string) reader[2];
            it.availableFlight.airplaneName = (string) reader[3];
            it.availableFlight.departureTime = (DateTime) reader[4];
            it.availableFlight.arrivalOffsetMinutes = (int) reader[5];
            it.optionsBin = (byte[]) reader[6];
            it.availableFlight.fromCode = (string) reader[7];
            it.availableFlight.toCode = (string) reader[8];
            it.bookedFlight.bookedPassangerCount = 1; //(int) reader[9];
            it.bookedFlight.bookingFinishedTime = (DateTime) reader[10];

            it.passangerId = (int) reader[11];
            it.passanger.archived = (bool) reader[12];
            it.passanger.name = (string) reader[13];
            it.passanger.surname = (string) reader[14];
            it.passanger.middleName = (string) (reader[15] is DBNull ? null : reader[15]);
            it.passanger.birthday = (DateTime) reader[16];
            it.documentBin = (byte[]) reader[17];

            it.selectedOptionsBin = (byte[]) reader[18];
            it.cancelled = (bool) reader[19];
            it.seatIndex = (short) reader[20];
            it.pnr = (string) reader[21];
            it.seatClass = (byte) reader[22];

            rawFlights.Add(it);
        }

        reader.Close();
        command.Dispose();
        cv.Dispose();

        var bookedFlights = new List<PassangerBookedFlight>(rawFlights.Count);

        for(int i = 0; i < rawFlights.Count; i++) {
            var rawFlight = rawFlights[i];

            rawFlight.passanger.document = DatabaseDocument.fromBytes(rawFlight.documentBin);

            var options = DatabaseOptions.optionsFromBytes(rawFlight.optionsBin);
            var selectedOptions = DatabaseOptions.selectedOptionsFromBytes(rawFlight.selectedOptionsBin);

            rawFlight.availableFlight.optionsForClasses = options;

            var seatAndOptions = new SeatAndOptions();
            seatAndOptions.seatIndex = selectedOptions.servicesOptions.seatSelected ? (int?) rawFlight.seatIndex : null;
            seatAndOptions.selectedOptions = selectedOptions;
            seatAndOptions.selectedSeatClass = rawFlight.seatClass;


            var costsArrResult = DatabaseFlights.calculateSeatsCosts(options, new SeatAndOptions[]{ seatAndOptions });
            if(!costsArrResult.IsSuccess) return Either<List<PassangerBookedFlight>, InputError>.Failure(
                new InputError("Ошибка вычисления цены: " + costsArrResult.f.message)
            );
            var costs = costsArrResult.s[0];

            var bookedSeat = new BookedSeatInfo();
            bookedSeat.pnr = rawFlight.pnr;
            bookedSeat.passangerId = rawFlight.passangerId;
            bookedSeat.selectedSeat = rawFlight.seatIndex;
            bookedSeat.cost = costs;

            bookedFlights.Add(new PassangerBookedFlight{
                cancelled = rawFlight.cancelled,
                flight = rawFlight.bookedFlight,
                passangerId = rawFlight.passangerId,
                passanger = rawFlight.passanger,
                bookedSeat = bookedSeat,
                seatAndOptions = seatAndOptions
            });
        }


        return Either<List<PassangerBookedFlight>, InputError>.Success(bookedFlights);
        }}}
    }

    public static Either<BookingFlightResult, LoginOrInputError> bookFlight(
        Account? account, SelectedSeat[] selectedSeats,
        Dictionary<int, Passanger> tempPassangers, int flightId
    ) {
        if(selectedSeats.Length == 0) return Either<BookingFlightResult, LoginOrInputError>.Failure(
            new LoginOrInputError{ InputError = new InputError(
                "Должен быть добавлен хотя бы один пассажир"
            ) }
        );

        var seatsAndOptions = new SeatAndOptions[selectedSeats.Length];
        for(int i = 0; i < seatsAndOptions.Length; i++) seatsAndOptions[i] = selectedSeats[i].seatAndOptions;

        //fill seats tables
        DataTable bookingTable = new DataTable();
        bookingTable.Columns.Add("Id", typeof(int));
        bookingTable.Columns.Add("TempPassanger", typeof(bool));
        bookingTable.Columns.Add("Passanger", typeof(int));
        bookingTable.Columns.Add("SelectedSeat", typeof(short));
        bookingTable.Columns.Add("Class", typeof(byte));
        bookingTable.Columns.Add("SelectedOptions", typeof(byte[]));

        ISet<int> addedTempPassangers = new HashSet<int>();

        DataTable tempPassangersTable = new DataTable();
        tempPassangersTable.Columns.Add("Id", typeof(int));
        tempPassangersTable.Columns.Add("Birthday", typeof(DateTime));
        tempPassangersTable.Columns.Add("Document", typeof(byte[]));
        tempPassangersTable.Columns.Add("Name", typeof(string));
        tempPassangersTable.Columns.Add("Surname", typeof(string));
        tempPassangersTable.Columns.Add("MiddleName", typeof(string));

        for(int i = 0; i < selectedSeats.Length; i++) {
            var seat = selectedSeats[i];

            var dr = bookingTable.NewRow();
            dr[0] = i;
            dr[1] = seat.fromTempPassangers;
            dr[2] = seat.passangerId;
            dr[3] = seat.seatAndOptions.seatIndex != null
                ? seat.seatAndOptions.seatIndex : (object) DBNull.Value;
            dr[4] = seat.seatAndOptions.selectedSeatClass;
            dr[5] = DatabaseOptions.selectedOptionsToBytes(seat.seatAndOptions.selectedOptions);
            bookingTable.Rows.Add(dr);

            if(seat.fromTempPassangers && !addedTempPassangers.Contains(seat.passangerId)) {
                addedTempPassangers.Add(seat.passangerId);

                var tp = tempPassangers[seat.passangerId];
                var tr = tempPassangersTable.NewRow();
                tr[0] = seat.passangerId;
                tr[1] = tp.birthday;
                tr[2] = DatabaseDocument.toBytes(tp.document);
                tr[3] = tp.name;
                tr[4] = tp.surname;
                tr[5] = tp.middleName != null ? tp.middleName : (object) DBNull.Value;

                tempPassangersTable.Rows.Add(tr);
            }
        }

        //create command
        using(
        var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {

        using(
        var bookFlight = new SqlCommand("[Flights].[BookFlight]", connection)) {
        bookFlight.CommandType = CommandType.StoredProcedure;

        var customerParam = bookFlight.Parameters.Add("@Customer", SqlDbType.Int);
        bookFlight.Parameters.AddWithValue("@AvailableFlight", flightId);
        var customerBookedFlightIdParam = bookFlight.Parameters.Add("@CustomerBookedFlightId", SqlDbType.Int);
        var errorAlreadyArchivedParam = bookFlight.Parameters.Add("@AlreadyArchived", SqlDbType.Bit);
        var errorSeatParam = bookFlight.Parameters.Add("@ErrorSeatId", SqlDbType.Int);
        var errorPassangerParam = bookFlight.Parameters.Add("@ErrorTempPassangerId ", SqlDbType.Int);
        customerBookedFlightIdParam.Direction = ParameterDirection.Output;
        errorAlreadyArchivedParam.Direction = ParameterDirection.Output;
        errorSeatParam.Direction = ParameterDirection.Output;
        errorPassangerParam.Direction = ParameterDirection.Output;


        var bookSeatsParam = bookFlight.Parameters.AddWithValue("@BookSeats", bookingTable);
        bookSeatsParam.SqlDbType = SqlDbType.Structured;
        bookSeatsParam.TypeName = "[Flights].[BookSeat]";

        var tempPassangersParam = bookFlight.Parameters.AddWithValue("@TempPassangers", tempPassangersTable);
        tempPassangersParam.SqlDbType = SqlDbType.Structured;
        tempPassangersParam.TypeName = "[Flights].[TempPassangers]";

        //verify account
        if(account != null) {
            var userIdRes = DatabaseAccount.getUserId(new SqlConnectionView(connection, false), (Account) account);
            if(!userIdRes.IsSuccess) return Either<BookingFlightResult, LoginOrInputError>.Failure(
                new LoginOrInputError{ LoginError = userIdRes.Failure() }
            );
            customerParam.Value = userIdRes.s;
        }
        else customerParam.Value = DBNull.Value;

        //calculate prices
        var optionsResult = DatabaseFlights.extractOptions(new SqlConnectionView(connection, false), flightId);
        if(!optionsResult.IsSuccess) return Either<BookingFlightResult, LoginOrInputError>.Failure(
            new LoginOrInputError{ InputError = optionsResult.f }
        );

        var costsResult = DatabaseFlights.calculateSeatsCosts(optionsResult.s, seatsAndOptions);
        if(!costsResult.IsSuccess) return Either<BookingFlightResult, LoginOrInputError>.Failure(
            new LoginOrInputError{ InputError = costsResult.f }
        );
        var costs = costsResult.s;

        //book flight
        var flightBookingResult = new DataSet();
        var dataAdapter = new SqlDataAdapter(bookFlight);
        dataAdapter.Fill(flightBookingResult);
        bookFlight.Dispose();
        connection.Dispose();

        //return expected errors
        var es = Validation.ErrorString.Create();

        if((bool) errorAlreadyArchivedParam.Value) {
            es.ac("на данный рейс уже невозможно оформить билеты");
        }
        if(!(errorSeatParam.Value is DBNull)) {
            var errorSeat = (int) errorSeatParam.Value;
            es.ac("место пассажира " + errorSeat + " уже занято или не существует");
        }
        if(!(errorPassangerParam.Value is DBNull)) {
            var errorPassanger = (int) errorPassangerParam.Value;
            es.ac("Пассажир " + errorPassanger + " не может быть добавлен");
        }

        if(es.Error) return Either<BookingFlightResult, LoginOrInputError>.Failure(
            new LoginOrInputError{ InputError = new InputError(es.Message) }
        );

        System.Diagnostics.Debug.Assert(
            flightBookingResult != null && flightBookingResult.Tables.Count == 3
            && flightBookingResult.Tables[0] != null && flightBookingResult.Tables[1] != null
            && flightBookingResult.Tables[2] != null
        );

        var customerBookedFlightId = customerBookedFlightIdParam.Value is DBNull ?
            null : (int?) customerBookedFlightIdParam.Value;

        //extract data from procedure
        Dictionary<int, int> localPassangersDatabaseIds; {
            var table = flightBookingResult.Tables[0];

            localPassangersDatabaseIds = new Dictionary<int, int>(table.Rows.Count);

            var localIds = table.Columns["LocalId"].Ordinal;
            var dbIds = table.Columns["DatabaseId"].Ordinal;
            for(var i = 0; i < table.Rows.Count; i++) {
                var row = table.Rows[i];
                localPassangersDatabaseIds.Add((int) row[localIds], (int) row[dbIds]);
            }
        }

        Dictionary<int, int> selectedSeatsIndices; {
            var table = flightBookingResult.Tables[1];

            selectedSeatsIndices = new Dictionary<int, int>(table.Rows.Count);

            var ids = table.Columns["Id"].Ordinal;
            var setIndices = table.Columns["SeatIndex"].Ordinal;
            for(var i = 0; i < table.Rows.Count; i++) {
                var row = table.Rows[i];
                selectedSeatsIndices.Add((int) row[ids], (int) row[setIndices]);
            }
        }

        Dictionary<int, string> passangersPNRs; {
            var table = flightBookingResult.Tables[2];

            passangersPNRs = new Dictionary<int, string>(table.Rows.Count);

            var ids = table.Columns["TempId"].Ordinal;
            var pnr = table.Columns["PNR"].Ordinal;
            for(var i = 0; i < table.Rows.Count; i++) {
                var row = table.Rows[i];
                passangersPNRs.Add((int) row[ids], (string) row[pnr]);
            }
        }

        //prepare result
        var result = new BookedSeatInfo[selectedSeats.Length];
        for(int i = 0; i < result.Length; i++) {
            var seat = selectedSeats[i];

            var bookingSeatInfo = new BookedSeatInfo();

            if(seat.fromTempPassangers) {
                bookingSeatInfo.passangerId = localPassangersDatabaseIds[seat.passangerId];
            }
            else bookingSeatInfo.passangerId = seat.passangerId;

            if(seat.seatAndOptions.seatIndex == null) {
                bookingSeatInfo.selectedSeat = selectedSeatsIndices[i];
            }
            else bookingSeatInfo.selectedSeat = (int) seat.seatAndOptions.seatIndex;

            bookingSeatInfo.pnr = passangersPNRs[i];
            bookingSeatInfo.cost = costs[i];

            result[i] = bookingSeatInfo;
        }

        return Either<BookingFlightResult, LoginOrInputError>.Success(new BookingFlightResult{
            customerBookedFlightId = customerBookedFlightId,
            seatsInfo = result
        });
        }}
    }

}
}
