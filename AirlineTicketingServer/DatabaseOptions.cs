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
			it.Write((byte) 0);
			it.Write(v.seatChoiceCostRub);
	    }
	
		public static Options ReadOptions(this BinaryReader it) {
			Debug.Assert(it.ReadByte() == 1);
			var baggageOptions = it.ReadBaggageOptions();
			var termsOptions = it.ReadTermsOptions();
			var servicesOptions = it.ReadServicesOptions();
			var basePrice = it.ReadInt32();
			return new Options{ 
				baggageOptions = baggageOptions, termsOptions = termsOptions, servicesOptions = servicesOptions,
				basePriceRub = basePrice
			};
	    }
		public static void Write(this BinaryWriter it, Options v) {
			it.Write((byte)1);
			it.Write(v.baggageOptions);
			it.Write(v.termsOptions);
			it.Write(v.servicesOptions);
			it.Write(v.basePriceRub);
	    }
	}

	public static class BinaryOptions {
		public static byte[] toBytes(Dictionary<int, Options> optionsForClasses) {
			using(
			MemoryStream ms = new MemoryStream()) { 
			using(
			BinaryWriter bw = new BinaryWriter(ms)) {

			bw.Write((byte) 0);
			bw.Write(optionsForClasses.Count);
			foreach(var classOptions in optionsForClasses) {
				bw.Write(classOptions.Key);
				bw.Write(classOptions.Value);
			}
			bw.Dispose();
			return ms.ToArray();
			}}
		}
	
		public static Dictionary<int, Options> fromBytes(byte[] bytes) {
			using(
			var stream = new MemoryStream(bytes, false)) { 
			using(
			var reader = new BinaryReader(stream)) {

			Debug.Assert(reader.ReadByte() == 0);
			var count = reader.ReadInt32();
			var result = new Dictionary<int, Options>(count);
			for(int i = 0; i < count; i++) { 
				var key = reader.ReadInt32();
				var value = reader.ReadOptions();
				result.Add(key, value);
			}
			Debug.Assert(stream.Position == stream.Length);
			return result;
			}}
		}

        public static byte[] toBytes(SelectedOptions it) {
			using(
			var ms = new MemoryStream(9)) {
			using(
			var st = new BinaryWriter(ms)) {

			st.Write((byte)0);
			st.Write(it.baggageOptions.baggageIndex);
			st.Write(it.baggageOptions.handLuggageIndex);
			
			return ms.ToArray();
			}}
        }
	}
}