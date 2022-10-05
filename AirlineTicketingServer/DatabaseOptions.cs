using FlightsOptions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace FlightsOptions {
	internal static class Ext {
		public static Size3 ReadSize3(this BinaryReader it) {
			Debug.Assert(it.ReadByte() == 0);
			var x = it.ReadInt16();
			var y = it.ReadInt16();
			var z = it.ReadInt16();
			return new Size3{ x = x, y = y, z = z };
	    }
	   
		 public static void Write(this BinaryWriter it, Size3 v) {
			it.Write((byte)0);
			it.Write(v.x);
			it.Write(v.y);
			it.Write(v.z);
	    }
	
		public static Baggage ReadBaggage(this BinaryReader it) {
			Debug.Assert(it.ReadByte() == 0);
			var costRub = it.ReadInt16();
			var count = it.ReadInt16();
			var maxWeight = it.ReadInt16();
			var maxDim = it.ReadSize3();
			return new Baggage(costRub: costRub, count: count, maxDim: maxDim, maxWeightKg: maxWeight);
	    }
	
	    public static void Write(this BinaryWriter it, Baggage v) {
			it.Write((byte)0);
			it.Write(v.costRub);
			it.Write(v.count);
			it.Write(v.maxWeightKg);
			it.Write(v.maxDim);
	    }
	
		public static BaggageOptions ReadBaggageOptions(this BinaryReader it) {
			Debug.Assert(it.ReadByte() == 0);
	
			var baggageCount = it.ReadByte();
			var baggage = new List<Baggage>(baggageCount);
			for(int i = 0; i < baggageCount; i++) baggage.Add(it.ReadBaggage());
	
			var handLuggageCount = it.ReadByte();
			var handLuggage = new List<Baggage>(handLuggageCount);
			for(int i = 0; i < handLuggageCount; i++) handLuggage.Add(it.ReadBaggage());
	
			return new BaggageOptions{ baggage = baggage, handLuggage = handLuggage };
	    }
		public static void Write(this BinaryWriter it, BaggageOptions v) {
			it.Write((byte)0);
	
			Debug.Assert(v.baggage.Count <= byte.MaxValue);
			it.Write((byte) v.baggage.Count);
			for(int i = 0; i < v.baggage.Count; i++) it.Write(v.baggage[i]);
	
			Debug.Assert(v.handLuggage.Count <= byte.MaxValue);
			it.Write((byte) v.handLuggage.Count);
			for(int i = 0; i < v.handLuggage.Count; i++) it.Write(v.handLuggage[i]);
	    }
	
		public static TermsOptions ReadTermsOptions(this BinaryReader it) {
			Debug.Assert(it.ReadByte() == 0);
			var changeFlightCostRub = it.ReadInt16();
			var refundCostRub = it.ReadInt16();
	
			return new TermsOptions{ changeFlightCostRub = changeFlightCostRub, refundCostRub = refundCostRub };
	    }
		public static void Write(this BinaryWriter it, TermsOptions v) {
			it.Write((byte)0);
			it.Write(v.changeFlightCostRub);
			it.Write(v.refundCostRub);
	    }
	
		public static ServicesOptions ReadServicesOptions(this BinaryReader it) {
			Debug.Assert(it.ReadByte() == 0);
			var seatChoiceCostRub = it.ReadInt16();
	
			return new ServicesOptions{ seatChoiceCostRub = seatChoiceCostRub };
	    }
		public static void Write(this BinaryWriter it, ServicesOptions v) {
			it.Write((byte)0);
			it.Write(v.seatChoiceCostRub);
	    }
	
		public static Options ReadOptions(this BinaryReader it) {
			Debug.Assert(it.ReadByte() == 0);
			var baggageOptions = it.ReadBaggageOptions();
			var termsOptions = it.ReadTermsOptions();
			var servicesOptions = it.ReadServicesOptions();
			return new Options{ baggageOptions = baggageOptions, termsOptions = termsOptions, servicesOptions = servicesOptions };
	    }
		public static void Write(this BinaryWriter it, Options v) {
			it.Write((byte)0);
			it.Write(v.baggageOptions);
			it.Write(v.termsOptions);
			it.Write(v.servicesOptions);
	    }
	}

	public static class Binary {
		public static byte[] toBytes(Options options) {
			MemoryStream ms = new MemoryStream();
			BinaryWriter bw = new BinaryWriter(ms);
	
			bw.Write(options);
	
			return ms.ToArray();
		}
	
		public static Options fromBytes(byte[] bytes) {
			var stream = new MemoryStream(bytes, false);
			var reader = new BinaryReader(stream);
			var result = reader.ReadOptions();
			Debug.Assert(stream.Position == stream.Length);
			return result;
		}
	}
}

namespace AirlineTicketingServer {
	public static class DatabaseOptions {
		public static void writeToDatabase(SqlConnectionView connView, Options options, int flightId, int classIndex) {
			using(
			var selectClasses = new SqlCommand(
				@"insert into [Flights].[FlightOptions]([FlightId], [Class], [Data])
				values (@FlightId, @Class, @Data)",
				connView.connection
			)) {
			selectClasses.CommandType = System.Data.CommandType.Text;
			selectClasses.Parameters.AddWithValue("@FlightId", flightId);
			selectClasses.Parameters.AddWithValue("@Class", classIndex);
			selectClasses.Parameters.AddWithValue("@Data", Binary.toBytes(options));
	
			connView.Open();
			selectClasses.ExecuteNonQuery();
			connView.Dispose();
			}
		}
	
		public static Options readFromDatabase(SqlConnectionView connView, int flightId, int classIndex) {
			using(
			var selectClasses = new SqlCommand(
				@"select top 1 [fo].[Data]
				from [Flights].[FlightOptions] as [fo]
				where [fo].[FlightId] = @FlightId and [fo].[Class] = @Class",
				connView.connection
			)) {
			selectClasses.CommandType = System.Data.CommandType.Text;
			selectClasses.Parameters.AddWithValue("@FlightId", flightId);
			selectClasses.Parameters.AddWithValue("@Class", classIndex);
	
			connView.Open();
			using(
			var result = selectClasses.ExecuteReader()) {
			Debug.Assert(result.Read());
			var arr = (byte[]) result[0];
			result.Close();
			connView.Dispose();
			return Binary.fromBytes(arr);
	
			}
			}
		}
	}
}
