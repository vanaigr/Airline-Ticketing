using FlightsOptions;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace FlightsOptions {
    internal static class Ext {
        public static Size3 ReadSize3(this BinaryReader it) {
            Common.Debug2.AssertPersistent(it.ReadByte() == 0);
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
            Common.Debug2.AssertPersistent(it.ReadByte() == 1);
            var costRub = it.ReadInt32();
            var count = it.ReadInt16();
            var maxWeight = it.ReadInt16();
            var maxDim = it.ReadSize3();
            return new Baggage(costRub: costRub, count: count, maxDim: maxDim, maxWeightKg: maxWeight);
        }

        public static void Write(this BinaryWriter it, Baggage v) {
            it.Write((byte)1);
            it.Write(v.costRub);
            it.Write(v.count);
            it.Write(v.maxWeightKg);
            it.Write(v.maxDim);
        }

        public static BaggageOptions ReadBaggageOptions(this BinaryReader it) {
            Common.Debug2.AssertPersistent(it.ReadByte() == 0);

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

            Common.Debug2.AssertPersistent(v.baggage.Count <= byte.MaxValue);
            it.Write((byte) v.baggage.Count);
            for(int i = 0; i < v.baggage.Count; i++) it.Write(v.baggage[i]);

            Common.Debug2.AssertPersistent(v.handLuggage.Count <= byte.MaxValue);
            it.Write((byte) v.handLuggage.Count);
            for(int i = 0; i < v.handLuggage.Count; i++) it.Write(v.handLuggage[i]);
        }

        public static TermsOptions ReadTermsOptions(this BinaryReader it) {
            Common.Debug2.AssertPersistent(it.ReadByte() == 1);
            var changeFlightCostRub = it.ReadInt32();
            var refundCostRub = it.ReadInt32();

            return new TermsOptions{ changeFlightCostRub = changeFlightCostRub, refundCostRub = refundCostRub };
        }
        public static void Write(this BinaryWriter it, TermsOptions v) {
            it.Write((byte)1);
            it.Write(v.changeFlightCostRub);
            it.Write(v.refundCostRub);
        }

        public static ServicesOptions ReadServicesOptions(this BinaryReader it) {
            Common.Debug2.AssertPersistent(it.ReadByte() == 2);
            var basePrice = it.ReadInt32();
            var seatChoiceCostRub = it.ReadInt32();

            return new ServicesOptions{
                basePriceRub = basePrice,
                seatChoiceCostRub = seatChoiceCostRub
            };
        }
        public static void Write(this BinaryWriter it, ServicesOptions v) {
            it.Write((byte) 2);
            it.Write(v.basePriceRub);
            it.Write(v.seatChoiceCostRub);
        }

        public static Options ReadOptions(this BinaryReader it) {
            Common.Debug2.AssertPersistent(it.ReadByte() == 2);
            var baggageOptions = it.ReadBaggageOptions();
            var termsOptions = it.ReadTermsOptions();
            var servicesOptions = it.ReadServicesOptions();
            return new Options{
                baggageOptions = baggageOptions,
                termsOptions = termsOptions,
                servicesOptions = servicesOptions,
            };
        }
        public static void Write(this BinaryWriter it, Options v) {
            it.Write((byte)2);
            it.Write(v.baggageOptions);
            it.Write(v.termsOptions);
            it.Write(v.servicesOptions);
        }
    }

    public static class DatabaseOptions {
        public static byte[] optionsToBytes(Dictionary<int, Options> optionsForClasses) {
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

        public static Dictionary<int, Options> optionsFromBytes(byte[] bytes) {
            using(
            var stream = new MemoryStream(bytes, false)) {
            using(
            var reader = new BinaryReader(stream)) {

            Common.Debug2.AssertPersistent(reader.ReadByte() == 0);
            var count = reader.ReadInt32();
            var result = new Dictionary<int, Options>(count);
            for(int i = 0; i < count; i++) {
                var key = reader.ReadInt32();
                var value = reader.ReadOptions();
                result.Add(key, value);
            }
            Common.Debug2.AssertPersistent(stream.Position == stream.Length);
            return result;
            }}
        }


        public static byte[] selectedOptionsToBytes(SelectedOptions it) {
            using(
            var ms = new MemoryStream(9)) {
            using(
            var st = new BinaryWriter(ms)) {

            st.Write((byte)1);
            st.Write(it.baggageOptions.baggageIndex);
            st.Write(it.baggageOptions.handLuggageIndex);
            st.Write(it.servicesOptions.seatSelected);

            return ms.ToArray();
            }}
        }

        public static SelectedOptions selectedOptionsFromBytes(byte[] bytes) {
            using(
            var stream = new MemoryStream(bytes, false)) {
            using(
            var reader = new BinaryReader(stream)) {

            Common.Debug2.AssertPersistent(reader.ReadByte() == 1);
            var baggageIndex = reader.ReadInt32();
            var handLuggageIndex = reader.ReadInt32();
            var seatSelected = reader.ReadBoolean();

            Common.Debug2.AssertPersistent(stream.Position == stream.Length);
            stream.Dispose();
            reader.Dispose();

            return new SelectedOptions(
                new SelectedBaggageOptions(
                    baggageIndex, handLuggageIndex
                ),
                new SelectedServicesOptions(
                    seatSelected
                )
            );
            }}
        }
    }
}
