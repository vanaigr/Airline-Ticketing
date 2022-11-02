using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	public struct PeriodicFlight {
		public int Id;
		public DateTime StartDate;
		public int StartTimeMinutes;
		public int DateRepeatPeriodDays;
		public int TimeRepeatPeriodMinutes;
		public int utcOffsetMinutes;
	}

	static class AvailableFlightsUpdate {
		public static readonly int maxDaysFuture = 10; //this day and future maxDaysFuture-1 days

        static readonly int minutesInDay = 60 * 24;

		public static void checkAndUpdate(SqlConnectionView connectionView) {	
			var thisDay = DateTime.UtcNow.Date;

			var connection = connectionView.connection;
			//archive old flights
			using(var archiveCommand = new SqlCommand(
				@"	
					declare @UpdatedFlights table(
						[AvailableFlight] int not null primary key
					);
					update [Flights].[AvailableFlights] 
					set [Archived] = 1
					output [inserted].[Id] into @UpdatedFlights
					where [DepartureDate] < @Today;

					delete [afs]
					from @UpdatedFlights as [uf]

					inner join [Flights].[AvailableFlightsSeats] as [afs]
					on [afs].[AvailableFlight] = [uf].[AvailableFlight]
						and [afs].[Passanger] is null;
				",
				connection
			)) {
			archiveCommand.CommandType = CommandType.Text;
			archiveCommand.Parameters.AddWithValue("@Today", thisDay);


			var addFlights = new DataTable();
			addFlights.Columns.Add("PeriodicFlightId", typeof(int));
			addFlights.Columns.Add("DepartureDate", typeof(DateTime));
			addFlights.Columns.Add("DepartureTimeMinutes", typeof(short));

			//fetch flights schedule
			using(var command = new SqlCommand(
				@"SELECT  
					[pfs].[Id], [pfs].[StartDate], [pfs].[StartTimeMinutes], 
					[pfs].[DateRepeatPeriodDays], [pfs].[TimeRepeatPeriodMinutes],
					[aps].[UTCDifferenceMinutes]
				FROM [Flights].[PeriodicFlightsSchedule] as [pfs]

				inner join [Flights].[FlightInfo] as [fi]
				on [pfs].[FlightInfo] = [fi].[Id]
				
				inner join [Flights].[Airports] as [aps]
				on [fi].[fromCity] = [aps].[Id]",
				connection
			)) {

			connectionView.Open();
			archiveCommand.ExecuteNonQuery();
			var daysPresent = calculateComputedDays(thisDay, new SqlConnectionView(connection, false));
			using(
			var result = command.ExecuteReader()) {
			command.Dispose();

			var repeatedFlights = new List<PeriodicFlight>();
			while(result.Read()) {
				repeatedFlights.Add(new PeriodicFlight{
					Id = (int) result[0],
					StartDate = (DateTime) result[1],
					StartTimeMinutes = (int) result[2],
					DateRepeatPeriodDays = (int) result[3],
					TimeRepeatPeriodMinutes = (int) result[4],
					utcOffsetMinutes = (int) result[5]
				});
			}
			result.Close();
			connection.Close();

			//calculate flights for available period
			foreach(var repeatedFlight in repeatedFlights) addAvailableFlightsFromPeriodicFlight(
				repeatedFlight, thisDay, daysPresent, 
				(int periodicFlightId, int dayOffset, int minutesOffsetInDay) => {
					var flightRow = addFlights.NewRow();
					flightRow[0] = periodicFlightId;
					flightRow[1] = thisDay.AddDays(dayOffset);
					flightRow[2] = (short) minutesOffsetInDay;
					addFlights.Rows.Add(flightRow);
				}
			);

			uploadAvailableFlights(connectionView, addFlights);
			}}}
		}

		static bool[] calculateComputedDays(DateTime thisDay, SqlConnectionView connectionView) {
			bool[] daysPresent = new bool[maxDaysFuture];

			//fill days that nedd to be updated
			using(var command = new SqlCommand(
				"SELECT DISTINCT [DepartureDate] FROM [Flights].[AvailableFlights]", 
				connectionView.connection
			)) {
				command.CommandType = CommandType.Text;
				using(var result = command.ExecuteReader()) {
					command.Dispose();
					while(result.Read()) {
						var diff = (DateTime)result[0] - thisDay;
						var dayOffset = diff.Days;
						if(dayOffset >= 0 && dayOffset < daysPresent.Length) {
							daysPresent[dayOffset] = true;
						}
					}
					connectionView.Dispose();
				}
			}

			return daysPresent;
		} 
		
		delegate void AddAvailableFlight(int periodicFlightId, int dayOffset, int minutesOffsetInDay);

		static void addAvailableFlightsFromPeriodicFlight(
			PeriodicFlight repeatedFlight, 
			DateTime availabilityPeriodStart,
			bool[] daysPresent,
			AddAvailableFlight addAvailableFlight
		) {
			Debug.Assert(repeatedFlight.DateRepeatPeriodDays > 0);

			var absoluteStartDate = repeatedFlight.StartDate.AddMinutes(-repeatedFlight.utcOffsetMinutes);

			var dTime = availabilityPeriodStart.Ticks - absoluteStartDate.Ticks;
			var flightPeriod = new DateTime().AddDays(repeatedFlight.DateRepeatPeriodDays).Ticks;

			var flightPeriodsBeforeAvailabilityStart = floorDiv(dTime, flightPeriod);
			var lastFlightBeforeAvailablePeriod = absoluteStartDate.AddDays(flightPeriodsBeforeAvailabilityStart * repeatedFlight.DateRepeatPeriodDays);
			
			var dayStart = lastFlightBeforeAvailablePeriod;

			while(true) {
                var flightMinutesOffset = repeatedFlight.StartTimeMinutes;
                
                while(flightMinutesOffset <= minutesInDay * repeatedFlight.DateRepeatPeriodDays) {
                    var thisFlightDatetime = dayStart.AddMinutes(flightMinutesOffset);

					var availabilityTimeDiff = (thisFlightDatetime - availabilityPeriodStart);
					var availabilityDayDiff = availabilityTimeDiff.Days;
					
					if(availabilityDayDiff >= maxDaysFuture) return;
                   
                    if(availabilityTimeDiff.Ticks >= 0 && !daysPresent[availabilityDayDiff]) addAvailableFlight(
						repeatedFlight.Id, availabilityDayDiff, 
						(int)((availabilityTimeDiff.Add(new TimeSpan(availabilityDayDiff, 0, 0, 0, 0)).Ticks
								% new TimeSpan(1, 0, 0, 0, 0).Ticks)
								/ new TimeSpan(0, 1, 0).Ticks)
					);

                    if(repeatedFlight.TimeRepeatPeriodMinutes == 0) break; /*
                        if period is not set then this flight is the only flight in given period
                    */
                    flightMinutesOffset += repeatedFlight.TimeRepeatPeriodMinutes;
                }
				
				dayStart += new TimeSpan(repeatedFlight.DateRepeatPeriodDays, 0, 0, 0, 0);
			}
		}

		static void uploadAvailableFlights(SqlConnectionView connectionView, DataTable flights) {
			var connection = connectionView.connection;

			using(var command = new SqlCommand(
				"[Flights].[InsertAvailableFlightsList]",
				connection
			)) {
				command.CommandType = CommandType.StoredProcedure;
				SqlParameter param = command.Parameters.AddWithValue("@availableFlights", flights);  
				param.SqlDbType = SqlDbType.Structured;  
				param.TypeName = "[Flights].[AvailableFlightInsert]";  

				connectionView.Open();
				command.ExecuteNonQuery(); //note: exception might be thrown
				command.Dispose();
				connectionView.Dispose();
			}
		}

		private static long floorDiv(long a, long b) {
			return a / b - Convert.ToInt32((a % b != 0) && ((a < 0) ^ (b < 0)));
		}

		private static long ceilDiv(long a, long b) {
			return -floorDiv(-a, b);
		}
	}
}
