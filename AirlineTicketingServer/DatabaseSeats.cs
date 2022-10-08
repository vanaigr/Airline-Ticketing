using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	static class BinarySeats {
		public static byte[] toBytes(SeatsScheme.Seats seats) {
			var length = seats.Width;
			var width = seats.Length;
			Debug.Assert(((short) length) == length);
			Debug.Assert(((short) width) == width);

			using(
			var stream = new MemoryStream(1*1 + 2*2 + 1*width*length)) {
			using(
			var writer = new BinaryWriter(stream)) {

			writer.Write((byte) 0);
			writer.Write((short) length);
			writer.Write((short) width);

			for(int z = 0; z < length; z++)
			for(int x = 0; x < width ; x++) {
				var seat = seats[x, z];
				Debug.Assert((seat.classIndex & 0xfu) == seat.classIndex);
				var data = seat.classIndex | (seat.occupied ? 1u << 7 : 0);
				writer.Write((byte) data);
			}
			writer.Dispose();
			return stream.ToArray();
			}}
		}

		public static SeatsScheme.Seats fromBytes(byte[] bytes) {
			using(
			var stream = new MemoryStream(bytes, false)) {
			using(
			var reader = new BinaryReader(stream)) {

			var version = reader.ReadByte();
			Debug.Assert(version == 0);
			var length = reader.ReadInt16();
			var width = reader.ReadInt16();

			var seats = new SeatsScheme.Seats(new SeatsScheme.SeatStatus[width * length], width);

			for(int z = 0; z < length; z++)
			for(int x = 0; x < width ; x++) {
				var data = reader.ReadByte();
				seats[x, z] = new SeatsScheme.SeatStatus{
					Class = (int)(data & 0xfu),
					occupied = (data & 0x1000_0000u) != 0
				};
			}

			Debug.Assert(stream.Position == stream.Length);
			reader.Dispose();
			stream.Dispose();
			return seats;
			}}
		}
	}

	static class DatabaseSeats {
		public static SeatsScheme.Seats readFromDatabaseAvailableFlights(SqlConnectionView cv, int flightId, int classId) {
			using(
			var selectClasses = new SqlCommand(
				@"select top 1 [fs].[Seats]
				from [Flights].[AvailableFlightsSeats] as [fs]
				where [fs].[AvailableFlight] = @FlightId and [fs].[Class] = @Class",
				cv.connection
			)) {
			selectClasses.CommandType = System.Data.CommandType.Text;
			selectClasses.Parameters.AddWithValue("@FlightId", flightId);
			selectClasses.Parameters.AddWithValue("@Class", classId);
	
			cv.Open();
			using(
			var result = selectClasses.ExecuteReader()) {
			if(!result.Read()) return null;
			var arr = (byte[]) result[0];
			result.Close();
			cv.Dispose();

			return BinarySeats.fromBytes(arr);
			}}
		}

		public static void writeToDatabaseAvailableFlights(SqlConnectionView connView, int flightId, SeatsScheme.Seats seats) {
			var arr = BinarySeats.toBytes(seats);
			
			using(
			var selectClasses = new SqlCommand(
				@"insert into [Flights].[AvailableFlightsSeats]([FlightId], [Seats])
				values (@FlightId, @Seats)",
				connView.connection
			)) {
			selectClasses.CommandType = System.Data.CommandType.Text;
			selectClasses.Parameters.AddWithValue("@FlightId", flightId);
			selectClasses.Parameters.AddWithValue("@Seats", arr);
	
			connView.Open();
			selectClasses.ExecuteNonQuery();
			connView.Dispose();
			}
		}

		public static void writeToDatabaseAirplanesSeats(SqlConnectionView connView, int airplane, SeatsScheme.Seats seats) {
			var arr = BinarySeats.toBytes(seats);
			
			using(
			var selectClasses = new SqlCommand(
				@"insert into [Flights].[AirplanesSeats]([Airplane], [StartingScheme])
				values (@Airplane, @StartingScheme)",
				connView.connection
			)) {
			selectClasses.CommandType = System.Data.CommandType.Text;
			selectClasses.Parameters.AddWithValue("@Airplane", airplane);
			selectClasses.Parameters.AddWithValue("@StartingScheme", arr);
	
			connView.Open();
			selectClasses.ExecuteNonQuery();
			connView.Dispose();
			}
		}
	}
}
