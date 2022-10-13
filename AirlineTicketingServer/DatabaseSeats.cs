using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	static class BinarySeats {
		public static byte[] toBytes(SeatsScheme.Seats seatsscheme) {
			
			using(
			var stream = new MemoryStream(1*1 + 1*2 + 1*2*seatsscheme.SizesCount + 1*seatsscheme.SeatsCount)) {
			using(
			var writer = new BinaryWriter(stream)) {

			writer.Write((byte) 0);
			Debug.Assert((short) seatsscheme.SizesCount == seatsscheme.SizesCount);
			writer.Write((short) seatsscheme.SizesCount);

			var sizes = seatsscheme.GetSizesEnumerator();
			while(sizes.MoveNext()) {
				var size = sizes.Current;
				Debug.Assert((byte) size.x == size.x);
				Debug.Assert((byte) size.z == size.z);
				writer.Write((byte) size.x);
				writer.Write((byte) size.z);
			}

			var seats = seatsscheme.GetSeatsEnumerator();
			while(seats.MoveNext()) {
				var seat = seats.Current;
				Debug.Assert((seat.classId & 0xfu) == seat.classId);
				var data = seat.classId | (seat.occupied ? 1u << 7 : 0);
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
			var sizesCount = reader.ReadInt16();
			var sizes = new List<SeatsScheme.Point>(sizesCount);
			var seatsSum = 0;
			for(int i = 0; i < sizesCount; i++) {
				var x = reader.ReadByte();
				var z = reader.ReadByte();
				sizes.Add(new SeatsScheme.Point{ x = x, z = z });
				seatsSum += x * z;
			}

			var seats =  new List<SeatsScheme.SeatStatus>(seatsSum);
			for(int i = 0; i < seatsSum; i++) {
				var data = reader.ReadByte();
				seats.Add(new SeatsScheme.SeatStatus{
					Class = (int)(data & 0xfu),
					occupied = (data & 0x10000000u) != 0
				});
			}

			var seatsScheme = new SeatsScheme.Seats(seats.GetEnumerator(), sizes.GetEnumerator());

			Debug.Assert(stream.Position == stream.Length);
			reader.Dispose();
			stream.Dispose();
			return seatsScheme;
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
