using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Server {
    static class DatabaseSeats {
        public static byte[] toBytes(FlightsSeats.SeatsScheme seatsScheme) {
            using(
            var stream = new MemoryStream(1*1 + 1*2 + 1*2*seatsScheme.SizesCount + 1*seatsScheme.SeatsCount)) {
            using(
            var writer = new BinaryWriter(stream)) {

            writer.Write((byte) 2);
            Common.Debug2.AssertPersistent((short) seatsScheme.SizesCount == seatsScheme.SizesCount);
            writer.Write((short) seatsScheme.SizesCount);

            var sizes = seatsScheme.GetSizesEnumerator();
            while(sizes.MoveNext()) {
                var size = sizes.Current;
                Common.Debug2.AssertPersistent((byte) size.x == size.x);
                Common.Debug2.AssertPersistent((byte) size.z == size.z);
                writer.Write((byte) size.x);
                writer.Write((byte) size.z);
            }

            writer.Dispose();
            return stream.ToArray();
            }}
        }

        public static FlightsSeats.SeatsScheme fromBytes(byte[] bytes) {
            using(
            var stream = new MemoryStream(bytes, false)) {
            using(
            var reader = new BinaryReader(stream)) {

            var version = reader.ReadByte();
            Common.Debug2.AssertPersistent(version == 2);
            var sizesCount = reader.ReadInt16();
            var sizes = new List<FlightsSeats.Point>(sizesCount);
            var seatsSum = 0;
            for(int i = 0; i < sizesCount; i++) {
                var x = reader.ReadByte();
                var z = reader.ReadByte();
                sizes.Add(new FlightsSeats.Point{ x = x, z = z });
                seatsSum += x * z;
            }

            Common.Debug2.AssertPersistent(stream.Position == stream.Length);
            reader.Dispose();
            stream.Dispose();
            return new FlightsSeats.SeatsScheme(sizes.GetEnumerator());
            }}
        }
    }
}
