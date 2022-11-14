using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Communication;
using OperatorCommunication;

namespace Server {

[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = false)]
class OperatorMessageService : OperatorService {
	Parameters OperatorService.parameters() {
		return new Parameters {
			flightClasses = Program.flightClasses,
			cities = Program.cities
		};
	}


	Either<List<Flight>, InputError> OperatorService.findMatchingFlights(MatchingFlightsParams p) {
		var err = Validation.ErrorString.Create();
		if(p.when == null) err.ac("дата вылета должа быть заполнена");

		if(err) return Either<List<Flight>, InputError>.Failure(new InputError(err.Message));
		else return Either<List<Flight>, InputError>.Success(DatabaseFlights.findMatchingFlights(p, mustBeAbeToBook: false));
	}

	class RawPassangerAndArrivalStatus {
		public string name, surname, middleName;
		public DateTime birthday;
		public byte[] documentBin;
		public short seatIndex;
		public bool canceled, arrived;
		public string pnr;
	}

	private struct RawSeat {
		public short index;
		public byte classId;
	};

	Either<FlightDetails, InputError> OperatorService.getFlightDetails(int flightId) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		using(
		var command = new SqlCommand(
			@"
			declare @Airplane int;

			select top 1 
				@Airplane = [ap].[Id]
			from (
				select top 1 [af].[FlightInfo]
				from [Flights].[AvailableFlights] as [af]
				where @AvailableFlight = [af].[Id]
			) as [afFlightInfo]
			
			inner join [Flights].[FlightInfo] as [fi]
			on [afFlightInfo].[FlightInfo] = [fi].[Id]

			inner join [Flights].[Airplanes] as [ap]
			on [fi].[Airplane] = [ap].[Id];


			--get seats scheme
			select top 1 [ap].[SeatsScheme]
			from [Flights].[Airplanes] as [ap]
			where [ap].[Id] = @Airplane;


			--get seats classes
			select 
				[aps].[SeatIndex],
				[aps].[Class] 
			from [Flights].[AirplanesSeats] as [aps]
			where [aps].[Airplane] = @Airplane
			order by [aps].[SeatIndex] ASC;


			--get passangers
			select 
				[afs].[SeatIndex],
				[afs].[Arrived],
				cast(case when [afs].[CanceledIndex] != 0 then 1 else 0 end as bit),
				[ps].[Name],
				[ps].[Surname],
				[ps].[MiddleName],
				[ps].[Birthday],
				[ps].[Document],
				[afs].[PNR]
			from (
				select *
				from [Flights].[AvailableFlightsSeats] as [afs]
				where [afs].[AvailableFlight] = @AvailableFlight
					and [afs].[Passanger] is not null
			) as [afs]

			inner join [Customers].[Passanger] as [ps]
			on [afs].[Passanger] = [ps].[Id]
			
			order by [afs].[SeatIndex] asc, [afs].[CanceledIndex] asc;
			" + DatabaseSeatsExtraction.commandText, 
			connection
		)) {
		command.CommandType = System.Data.CommandType.Text;
		command.Parameters.AddWithValue("@AvailableFlight", flightId);

		var rawPassangers = new List<RawPassangerAndArrivalStatus>();
		var rawSeatsClasses = new List<RawSeat>();
		
		connection.Open();
		using(
		var result = command.ExecuteReader()) {
		
		if(!result.Read()) return Either<FlightDetails, InputError>.Failure(new InputError(
			"Данный рейс не найден"
		));
		var seatsSchemeBin = (byte[]) result[0];

		Common.Debug2.AssertPersistent(result.NextResult());
		while(result.Read()) rawSeatsClasses.Add(new RawSeat{
			index = (short) result[0],
			classId = (byte) result[1],
		});
		
		Common.Debug2.AssertPersistent(result.NextResult());

		while(result.Read()) {
			rawPassangers.Add(new RawPassangerAndArrivalStatus{
				seatIndex = (short) result[0],
				arrived = (bool) result[1],
				canceled = (bool) result[2],
				name = (string) result[3],
				surname = (string) result[4],
				middleName = (string) result[5],
				birthday = (DateTime) result[6],
				documentBin = (byte[]) result[7],
				pnr = (string) result[8]
			});
		}

		result.Close();
		command.Dispose();
		connection.Dispose();
		
		var scheme = DatabaseSeats.fromBytes(seatsSchemeBin);
		var passangers = new List<BookedPassanger>(rawPassangers.Count);
		var arrivalStatus = new List<bool>(rawPassangers.Count);
		var classes = new byte[scheme.SeatsCount];

		for(int i = 0; i < rawPassangers.Count; i++) {
			var it = rawPassangers[i];
			passangers.Add(new BookedPassanger{
				canceled = it.canceled,
				seatIndex = it.seatIndex,
				name = it.name,
				surname = it.surname,
				middleName = it.middleName,
				birthday = it.birthday,
				document = DatabaseDocument.fromBytes(it.documentBin),
				pnr = it.pnr
			});
			arrivalStatus.Add(it.arrived);
		}

		for(int i = 0; i < classes.Length; i++) {
			Common.Debug2.AssertPersistent(rawSeatsClasses[i].index == i);
			classes[i] = rawSeatsClasses[i].classId;
		}

		return Either<FlightDetails, InputError>.Success(new FlightDetails{ 
			seats = scheme,
			seatsClasses = classes,
			passangersAndSeats = passangers,
			passangerArrived = arrivalStatus
		});
		}}}
	}

	Either<Success, InputError> OperatorService.updateArrivalStatus(int flightId, Dictionary<int, bool> seatArrived) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		using(
		var command = new SqlCommand(
			@"
			set xact_abort on
			begin tran

			update [afs]
			set [Arrived] = [sa].[Arrived]
			from @SeatsArrival as [sa]
			
			left join (
				select * 
				from [Flights].[AvailableFlightsSeats] as [afs]
				where [afs].[AvailableFlight] = @AvailableFlight
					and [afs].[CanceledIndex] = 0
					and [afs].[Passanger] is not null
			) as [afs]
			on [sa].[SeatIndex] = [afs].[SeatIndex]


			if(@@rowcount = (select count(*) from @SeatsArrival)) begin
				commit tran;
				select cast(1 as bit);
			end
			else begin
				rollback tran;
				select cast(0 as bit);
			end
			",
			connection
		)){
		command.CommandType = System.Data.CommandType.Text;
		command.Parameters.AddWithValue("@AvailableFlight", flightId);

		var seatsArrival = new DataTable();
		seatsArrival.Columns.Add("SeatIndex", typeof(short));
		seatsArrival.Columns.Add("Arrived", typeof(bool));

		foreach(var pair in seatArrived) {
			var row = seatsArrival.NewRow();
			row[0] = (short) pair.Key;
			row[1] = pair.Value;
			seatsArrival.Rows.Add(row);
		}

		var seatsArrivalParam = command.Parameters.AddWithValue("@SeatsArrival", seatsArrival);
		seatsArrivalParam.SqlDbType = SqlDbType.Structured;
		seatsArrivalParam.TypeName = "[Flights].[SeatsArrival]";

		connection.Open();
		using(
		var result = command.ExecuteReader()) { 
		if(!result.Read()) return Either<Success, InputError>.Failure(
			new InputError("Данный рейс не найден")
		);
		var ok = (bool) result[0];
		result.Close();
		command.Dispose();
		connection.Dispose();

		if(ok) return Either<Success, InputError>.Success(new Success());
		else return Either<Success, InputError>.Failure(
			new InputError("Ошибка изменения")
		);
		}}}
	}

	Either<List<PassangerBookedFlight>, InputError> OperatorService.getPassangerBookedFlights(PassangerSearchParams ps) {
		using(var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		return SeatsBooking.getBookedFlight(new SqlConnectionView(connection, true), ps);
		}
	}

	Either<Success, InputError> OperatorService.deleteBookedSeat(string pnr) {
		return SeatsBooking.deleteBookedSeat(null, pnr);
	}
}
}
