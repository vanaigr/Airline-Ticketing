using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	static class BinarySeats {
		public static byte[] toBytes(FlightsSeats.SeatsScheme seatsScheme) {
			using(
			var stream = new MemoryStream(1*1 + 1*2 + 1*2*seatsScheme.SizesCount + 1*seatsScheme.SeatsCount)) {
			using(
			var writer = new BinaryWriter(stream)) {

			writer.Write((byte) 2);
			Debug.Assert((short) seatsScheme.SizesCount == seatsScheme.SizesCount);
			writer.Write((short) seatsScheme.SizesCount);

			var sizes = seatsScheme.GetSizesEnumerator();
			while(sizes.MoveNext()) {
				var size = sizes.Current;
				Debug.Assert((byte) size.x == size.x);
				Debug.Assert((byte) size.z == size.z);
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
			Debug.Assert(version == 2);
			var sizesCount = reader.ReadInt16();
			var sizes = new List<FlightsSeats.Point>(sizesCount);
			var seatsSum = 0;
			for(int i = 0; i < sizesCount; i++) {
				var x = reader.ReadByte();
				var z = reader.ReadByte();
				sizes.Add(new FlightsSeats.Point{ x = x, z = z });
				seatsSum += x * z;
			}

			Debug.Assert(stream.Position == stream.Length);
			reader.Dispose();
			stream.Dispose();
			return new FlightsSeats.SeatsScheme(sizes.GetEnumerator());
			}}
		}
	}
}
