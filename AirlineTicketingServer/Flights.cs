using Communication;
using FlightsOptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	static class Flights {
		struct RawAvailableFlight {
			public int id;
			public DateTime departureTime;
			public int arrivalOffsteMinutes;

			public string flightName;
			public string airplaneName;

			public byte[] optionsBin;
			public int[] seatsCountForClasses;
			public int[] availableSeatsForClasses;
		}

		public static List<AvailableFlight> matchingFlights(MatchingFlightsParams p, bool mustBeAbeToBook) {			
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
					[AvailableFirstClassSeatsCount]
				from [Flights].[FindFlights](@fromCity, @toCity, @time, @MustBeAbleToBook) 
				order by [DepartureDatetime] desc",
				connection
			)) {
			selectClasses.CommandType = CommandType.Text;
			selectClasses.Parameters.AddWithValue("@fromCity", p.fromCode);
			selectClasses.Parameters.AddWithValue("@toCity", p.toCode);
			selectClasses.Parameters.AddWithValue("@time", p.when);
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
				}
			});
			}}}

			var list = new List<AvailableFlight>(rawAvailableFlights.Count);
			for(int i = rawAvailableFlights.Count-1; i >= 0 ; i--) {
				var raf = rawAvailableFlights[i];
				rawAvailableFlights.RemoveAt(i);
				list.Add(new AvailableFlight{
					id = raf.id,
					departureTime = raf.departureTime,
					arrivalOffsteMinutes = raf.arrivalOffsteMinutes,

					flightName = raf.flightName,
					airplaneName = raf.airplaneName,

					optionsForClasses = BinaryOptions.fromBytes(raf.optionsBin),
					seatCountForClasses = raf.seatsCountForClasses,
					availableSeatsForClasses = raf.availableSeatsForClasses
				});
			}

			return list;
		}
	}
}
