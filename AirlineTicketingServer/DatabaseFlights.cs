using Communication;
using FlightsOptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Server {
static class DatabaseFlights {
	struct RawAvailableFlight {
		public int id;
		public DateTime departureTime;
		public int arrivalOffsteMinutes;

		public string flightName;
		public string airplaneName;

		public string fromCode, toCode;

		public byte[] optionsBin;
		public int[] seatsCountForClasses;
		public int[] availableSeatsForClasses;
	}

	public static List<Flight> findMatchingFlights(MatchingFlightsParams p, bool mustBeAbeToBook) {			
		var rawAvailableFlights = new List<RawAvailableFlight>();

		using(
		var connection = new SqlConnection(Properties.Settings.Default.customersFlightsConnection)) {
		using(
		var selectClasses = new SqlCommand(
			@"select 
				[AvailableFlight], [FlightName], [AirplaneName], [DepartureDatetime], 
				[ArivalOffsetMinutes], [OptionsBin],

				[EconomyClassSeatsCount],
				[ComfortClassSeatsCount],
				[BusinessClassSeatsCount],
				[FirstClassSeatsCount],

				[AvailableEconomyClassSeatsCount], 
				[AvailableComfortClassSeatsCount],
				[AvailableBusinessClassSeatsCount],
				[AvailableFirstClassSeatsCount],

				[fromCode], [toCode]
			from [Flights].[FindFlights](@fromCity, @toCity, @time, @MustBeAbleToBook) 
			order by [DepartureDatetime] asc",
			connection
		)) {
		selectClasses.CommandType = CommandType.Text;
		selectClasses.Parameters.AddWithValue("@fromCity", p.fromCode ?? (object) DBNull.Value);
		selectClasses.Parameters.AddWithValue("@toCity", p.toCode ?? (object) DBNull.Value);
		selectClasses.Parameters.AddWithValue("@time", (object) p.when ?? (object) DBNull.Value);
		selectClasses.Parameters.AddWithValue("@MustBeAbleToBook", mustBeAbeToBook);

		connection.Open();
		using(
		var result = selectClasses.ExecuteReader()) {
		while(result.Read()) rawAvailableFlights.Add(new RawAvailableFlight{
			id = (int) result[0], flightName = (string) result[1],
			airplaneName = (string) result[2], departureTime = (DateTime) result[3],
			arrivalOffsteMinutes = (int) result[4],
			optionsBin = (byte[]) result[5],
			seatsCountForClasses = new int[]{
				(short) result[6], (short) result[7],
				(short) result[8], (short) result[9],
			},
			availableSeatsForClasses = new int[]{
				(short) result[10], (short) result[11],
				(short) result[12], (short) result[13],
			},
			fromCode = (string) result[14],
			toCode = (string) result[15],
		});
		}}}

		var list = new List<Flight>(rawAvailableFlights.Count);
		for(int i = 0; i < rawAvailableFlights.Count; i++) {
			var raf = rawAvailableFlights[i];
			list.Add(new Flight{
				id = raf.id,
				departureTime = raf.departureTime,
				arrivalOffsetMinutes = raf.arrivalOffsteMinutes,

				fromCode = raf.fromCode,
				toCode = raf.toCode,

				flightName = raf.flightName,
				airplaneName = raf.airplaneName,

				optionsForClasses = DatabaseOptions.optionsFromBytes(raf.optionsBin),
				seatCountForClasses = raf.seatsCountForClasses,
				availableSeatsForClasses = raf.availableSeatsForClasses
			});
		}

		return list;
	}

	public static Either<SeatCost[], InputError> calculateSeatsCosts(
		Dictionary<int, Options> options, SeatAndOptions[] seats
	) {
		var seatsCost = new SeatCost[seats.Length];

		for(int i = 0; i < seats.Length; i++) {
			var seat = seats[i];
			var opt = options[seat.selectedSeatClass];

			var hi = seat.selectedOptions.baggageOptions.handLuggageIndex;
			var bi = seat.selectedOptions.baggageOptions.baggageIndex;
			if(
				bi < 0 || bi >= opt.baggageOptions.baggage.Count ||
				hi < 0 || hi >= opt.baggageOptions.handLuggage.Count
			) return Either<SeatCost[], InputError>.Failure( 
				new InputError{ message = "для пассажира" + (seats.Length == 1 ? "" : " " + i) + " не заданы опции для багажа" }
			);

			if(seat.selectedOptions.servicesOptions.seatSelected != (seat.seatIndex != null)) {
				return Either<SeatCost[], InputError>.Failure(new InputError{ 
						message = "для пассажира" + (seats.Length == 1 ? "" : " " + i) 
						+ " неправильно задана опция автовыбора места" 
				});
			}

			var baggageOption = opt.baggageOptions.baggage[bi];
			var handLuggageOption = opt.baggageOptions.handLuggage[hi];

			var sd = new SeatCost(){
				basePrice = opt.servicesOptions.basePriceRub,
				baggageCost = baggageOption.costRub + handLuggageOption.costRub,
				seatCost = opt.servicesOptions.seatChoiceCostRub * (seat.selectedOptions.servicesOptions.seatSelected ? 1 : 0)
			};

			sd.totalCost = sd.basePrice + sd.baggageCost + sd.seatCost;

			seatsCost[i] = sd;
		}

		return Either<SeatCost[], InputError>.Success(seatsCost);
	}

	public static Either<Dictionary<int, Options>, InputError> extractOptions(
		SqlConnectionView cv, int availableFlightId
	) {
		using(cv) {
		using(
		var command = new SqlCommand(
			@"select top 1 [fi].[Options]
			from (
				select * from [Flights].[AvailableFlights] as [af]
				where [af].[Id] = @AvailableFlight
			) as [af]

			inner join [Flights].[FlightInfo] as [fi]
			on [af].[FlightInfo] = [fi].[Id]
			
			inner join [Flights].[Airplanes] as [ap]
			on [fi].[Airplane] = [ap].[Id]",
			cv.connection
		)) {
		command.CommandType = CommandType.Text;
		command.Parameters.AddWithValue("@AvailableFlight", availableFlightId);

		cv.Open();
		using(
		var result = command.ExecuteReader()) {

		if(!result.Read()) return Either<Dictionary<int, Options>, InputError>.Failure(new InputError(
			"Выбранного рейса не существует"
		));

		var optionsBin = (byte[]) result[0];

		result.Close();
		command.Dispose();
		cv.Dispose();

		var options = DatabaseOptions.optionsFromBytes(optionsBin);

		return Either<Dictionary<int, Options>, InputError>.Success(options);
		}}}
	}
}
}
