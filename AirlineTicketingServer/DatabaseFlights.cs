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
				arrivalOffsteMinutes = raf.arrivalOffsteMinutes,

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
}
}
