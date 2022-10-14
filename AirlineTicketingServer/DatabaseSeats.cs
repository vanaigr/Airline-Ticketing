using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace AirlineTicketingServer {
	public struct SeatsSchemeAndClasses {
		public SeatsScheme.SeatsScheme scheme;
		public byte[] classes;
	}
	static class BinarySeats {
		public static byte[] toBytes(SeatsSchemeAndClasses sac) {
			SeatsScheme.SeatsScheme seatsscheme = sac.scheme;
			byte[] classes = sac.classes;
			Debug.Assert(seatsscheme.SeatsCount == classes.Length);

			using(
			var stream = new MemoryStream(1*1 + 1*2 + 1*2*seatsscheme.SizesCount + 1*seatsscheme.SeatsCount)) {
			using(
			var writer = new BinaryWriter(stream)) {

			writer.Write((byte) 1);
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

			foreach(var seatClass in classes) writer.Write(seatClass);

			writer.Dispose();
			return stream.ToArray();
			}}
		}

		public static SeatsSchemeAndClasses fromBytes(byte[] bytes) {
			using(
			var stream = new MemoryStream(bytes, false)) {
			using(
			var reader = new BinaryReader(stream)) {

			var version = reader.ReadByte();
			Debug.Assert(version == 1);
			var sizesCount = reader.ReadInt16();
			var sizes = new List<SeatsScheme.Point>(sizesCount);
			var seatsSum = 0;
			for(int i = 0; i < sizesCount; i++) {
				var x = reader.ReadByte();
				var z = reader.ReadByte();
				sizes.Add(new SeatsScheme.Point{ x = x, z = z });
				seatsSum += x * z;
			}

			var seats =  new List<byte>(seatsSum);
			for(int i = 0; i < seatsSum; i++) seats.Add(reader.ReadByte());

			var seatsSchemeAndClasses = new SeatsSchemeAndClasses{
				scheme = new SeatsScheme.SeatsScheme(sizes.GetEnumerator()),
				classes = seats.ToArray()
			};

			Debug.Assert(stream.Position == stream.Length);
			reader.Dispose();
			stream.Dispose();
			return seatsSchemeAndClasses;
			}}
		}
	}
}
