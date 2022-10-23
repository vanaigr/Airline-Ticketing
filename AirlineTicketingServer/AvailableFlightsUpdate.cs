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
	}

	static class AvailableFlightsUpdate {
		public static readonly int maxDaysFuture = 10; //this day and future maxDaysFuture-1 days

        static readonly int minutesInDay = 60 * 24;

		public static void checkAndUpdate(SqlConnectionView connectionView) {	
			var thisDay = DateTime.Today;

			var connection = connectionView.connection;
			//delete old flights
			using(var command = new SqlCommand(
				@"[Flights].[RemoveOldFlights]",
				connection
			)) {
				command.CommandType = System.Data.CommandType.StoredProcedure;
				command.Parameters.Add(new SqlParameter("@Today", SqlDbType.Date));
				command.Parameters["@Today"].Value = thisDay; /*
					date is passed by parameter because
					database date can be different from server date
				*/
				connectionView.Open();
				var result = command.ExecuteNonQuery();
			}

			var daysPresent = calculateComputedDays(thisDay, new SqlConnectionView(connection, false));

			var addFlights = new DataTable();
			addFlights.Columns.Add("PeriodicFlightId", typeof(int));
			addFlights.Columns.Add("DepartureDate", typeof(DateTime));
			addFlights.Columns.Add("DepartureTimeMinutes", typeof(short));

			//fetch flights schedule
			using(var command = new SqlCommand(
				@"SELECT  [Id], [StartDate], [StartTimeMinutes], [DateRepeatPeriodDays], [TimeRepeatPeriodMinutes]
				FROM [Flights].[PeriodicFlightsSchedule]",
				connection
			)) {
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
					TimeRepeatPeriodMinutes = (int) result[4]
				});
			}
			result.Close();

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
			}
			}

			uploadAvailableFlights(connectionView, addFlights);
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

			var dTime = availabilityPeriodStart.Ticks - repeatedFlight.StartDate.Ticks;
			var flightPeriod = new DateTime().AddDays(repeatedFlight.DateRepeatPeriodDays).Ticks;
			var flightPeriodsCount = dTime / flightPeriod;
			var firstFlightInAvailablePeriod = repeatedFlight.StartDate.AddDays(flightPeriodsCount * repeatedFlight.DateRepeatPeriodDays);
			
			var flightDayOffset = (firstFlightInAvailablePeriod - availabilityPeriodStart).Days;

			while(true) {
                var flightMinutesOffset = repeatedFlight.StartTimeMinutes;
                
                while(flightMinutesOffset <= minutesInDay * repeatedFlight.DateRepeatPeriodDays) {
                    var thisFlightDayOffsetFromOffset = flightMinutesOffset / minutesInDay;
                    var thisFlightMinutesOffsetInDay = flightMinutesOffset % minutesInDay;
                    var thisFlightDayOffset = flightDayOffset + thisFlightDayOffsetFromOffset;

					if(thisFlightDayOffset >= maxDaysFuture) return;
                   
                    if(!daysPresent[thisFlightDayOffset]) {
					    addAvailableFlight(repeatedFlight.Id, thisFlightDayOffset, thisFlightMinutesOffsetInDay);
				    }

                    if(repeatedFlight.TimeRepeatPeriodMinutes == 0) break; /*
                        if period is not set then this flight is the only flight in given period
                    */
                    flightMinutesOffset += repeatedFlight.TimeRepeatPeriodMinutes;
                }
				

				flightDayOffset += repeatedFlight.DateRepeatPeriodDays;
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

				command.ExecuteNonQuery(); //note: exception might be thrown
				command.Dispose();
				connectionView.Dispose();
			}
		}
	}
}
