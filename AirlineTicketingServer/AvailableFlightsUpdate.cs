using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	struct RepeatedFlight {
		public int Id;
		public DateTime StartDate;
		public int StartTimeMinutes;
		public int DateRepeatPeriodDays;
		public int TimeRepeatPeriodMinutes;
	}

	class AvailableFlightsUpdate {
		public static readonly int maxDaysFuture = 10; //this day and future maxDaysFuture-1 days

        static readonly int minutesInDay = 60 * 24;

		public static void checkAndUpdate(SqlConnectionView connectionView) {	
			var thisDay = DateTime.Today;

			var connection = connectionView.connection;
			//delete old flights
			using(var command = new SqlCommand(
				@"DELETE FROM [Flights].[AvailableFlights] 
				  WHERE [DepartureDate] < @Today",
				connection
			)) {
				command.CommandType = System.Data.CommandType.Text;
				command.Parameters.Add(new SqlParameter("@Today", System.Data.SqlDbType.Date));
				command.Parameters["@Today"].Value = thisDay; /*
					date is passed by parameter because
					database date can be different from server date
				*/

				connection.Open();
				var result = command.ExecuteNonQuery();
			}

			bool[] daysPresent = new bool[maxDaysFuture];

			//fill days that nedd to be updated
			using(var command = new SqlCommand(
				"SELECT DISTINCT [DepartureDate] FROM [Flights].[AvailableFlights]", 
				connection
			)) {
				command.CommandType = CommandType.Text;
				using(
				var result = command.ExecuteReader()) {
				command.Dispose();
				while(result.Read()) {
					var diff = (DateTime)result[0] - thisDay;
					var dayOffset = diff.Days;
					if(dayOffset >= 0 && dayOffset < daysPresent.Length) {
						daysPresent[dayOffset] = true;
					}
				}
				}
			}

			//var addFlights = new List<AvailableFlight>();
			var addFlights = new DataTable();
			addFlights.Columns.Add("DepartureDate", typeof(DateTime));
			addFlights.Columns.Add("DepartureTimeMinutes", typeof(int));
			addFlights.Columns.Add("PeriodicFlightId", typeof(int));

			//calculate new flights in available period
			using(var command = new SqlCommand(
				@"SELECT  [Id], [StartDate], [StartTimeMinutes], [DateRepeatPeriodDays], [TimeRepeatPeriodMinutes]
				FROM [Flights].[PeriodicFlightsSchedule]",
				connection
			)) {
				using(
				var result = command.ExecuteReader()) {
				command.Dispose();

				var repeatedFlights = new List<RepeatedFlight>();
				while(result.Read()) {
					repeatedFlights.Add(new RepeatedFlight{
						Id = (int) result[0],
						StartDate = (DateTime) result[1],
						StartTimeMinutes = (int) result[2],
						DateRepeatPeriodDays = (int) result[3],
						TimeRepeatPeriodMinutes = (int) result[4]
					});
				}
				result.Close();

				var availabilityPeriodStartDate = thisDay;
				var availabilityPeriodStart = availabilityPeriodStartDate.Ticks;

				foreach(var repeatedFlight in repeatedFlights) {
					Debug.Assert(repeatedFlight.DateRepeatPeriodDays > 0);

					var dTime = availabilityPeriodStart - repeatedFlight.StartDate.Ticks;
					var flightPeriod = new DateTime().AddDays(repeatedFlight.DateRepeatPeriodDays).Ticks;
					var flightPeriodsCount = dTime / flightPeriod;
					var firstFlightInAvailablePeriod = repeatedFlight.StartDate.AddDays(flightPeriodsCount * repeatedFlight.DateRepeatPeriodDays);
					
					var flightDayOffset = (firstFlightInAvailablePeriod - availabilityPeriodStartDate).Days;

					while(flightDayOffset < maxDaysFuture) {
                        var flightMinutesOffset = repeatedFlight.StartTimeMinutes;
                        
                        while(flightMinutesOffset <= minutesInDay * repeatedFlight.DateRepeatPeriodDays) {
                            var thisFlightDayOffsetFromOffset = flightMinutesOffset / minutesInDay;
                            var thisFlightMinutesOffsetInDay = flightMinutesOffset % minutesInDay;
                            var thisFlightDayOffset = flightDayOffset + thisFlightDayOffsetFromOffset;
                           
                            if(!daysPresent[thisFlightDayOffset]) {
							    var flightRow = addFlights.NewRow();
							    flightRow[0] = availabilityPeriodStartDate.AddDays(thisFlightDayOffset);
							    flightRow[1] = thisFlightMinutesOffsetInDay;
							    flightRow[2] = repeatedFlight.Id;
							    addFlights.Rows.Add(flightRow);
						    }

                            if(repeatedFlight.TimeRepeatPeriodMinutes == 0) break; /*
                                if period is not set then this flight is the only flight in given period
                            */
                            flightMinutesOffset += repeatedFlight.TimeRepeatPeriodMinutes;
                        }
						

						flightDayOffset += repeatedFlight.DateRepeatPeriodDays;
					}
				}

				}
			}

			//update flights in available period
			using(var command = new SqlCommand(
				"[Flights].[InsertAvailableFlightsList]",
				connection
			)) {
				command.CommandType = CommandType.StoredProcedure;
				SqlParameter param = command.Parameters.AddWithValue("@availableFlights", addFlights);  
				param.SqlDbType = SqlDbType.Structured;  
				param.TypeName = "[Flights].[AvailableFlightInsert]";  

				command.ExecuteNonQuery();
				command.Dispose();
				connectionView.Dispose();
			}
		}
	}
}
