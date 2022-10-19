using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SeatsScheme {
	[Serializable] public struct Point { public int x, z; public Point(int z, int x) { this.x = x; this.z = z; } }

	[Serializable] public class SeatsScheme {
		List<int> seatsBeforeZ;
		List<int> sizeIndexForZ;
		List<Point> sizes;
		int totalLength;
		int seatsCount;

		public SeatsScheme(IEnumerator<Point> sizesE) { setFromIterators(sizesE); }

		public int WidthForRow(int z) { return sizes[sizeIndexForZ[z]].x; }

		public int SeatsBeforeZ(int z) { return seatsBeforeZ[z]; }

		public int TotalLength => totalLength;
		public int SeatsCount => seatsCount;
		public int SizesCount => sizes.Count;
		
		public int coordToIndex(int x, int z) {
			Debug.Assert(x >= 0 && x < WidthForRow(z));
			return SeatsBeforeZ(z) + x;
		}

		public int coordToIndex(Point coord) {
			return coordToIndex(coord.x, coord.z);
		}

		public Point indexToCoord(int index) {
			if(index < 0 || index >= seatsCount) {
				throw new IndexOutOfRangeException();
			}
			var prevBefore = 0;
			for(int z = 1; z < totalLength; z++) {
				var beforeZ = seatsBeforeZ[z];
				if(beforeZ > index) {
					return new Point{ x = index - prevBefore, z = z-1 };
				}
				prevBefore = beforeZ;
			}
			if(SeatsCount > index) return new Point { x = index - prevBefore, z = totalLength - 1 };
			throw new IndexOutOfRangeException();
		}

		public Point sizeAtIndex(int i) { return sizes[i]; }

		public IEnumerator<Point> GetSizesEnumerator() {
			return sizes.GetEnumerator();
		}
		
		protected SeatsScheme(SerializationInfo info, StreamingContext context) {
			Debug.Assert(info.GetInt32("version") == 1);
			var sizes = (List<Point>) info.GetValue("sizes", typeof(List<Point>));
			setFromIterators(sizes.GetEnumerator());
		}

		public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("version", 1);
			info.AddValue("sizes", sizes, typeof(List<Point>));
		}

		private void setFromIterators(
			IEnumerator<Point> sizesE
		) {
			this.seatsBeforeZ = new List<int>();
			this.sizeIndexForZ = new List<int>();
			this.sizes = new List<Point>();

			int seatsSum = 0;
			for(int i = 0; sizesE.MoveNext(); i++) {
				var size = sizesE.Current;
				
				for(int z = 0; z < size.z; z++) this.sizeIndexForZ.Add(i);
				for(int z = 0; z < size.z; z++) {
					this.seatsBeforeZ.Add(seatsSum);
					seatsSum += size.x;
				}
				this.totalLength += size.z;
				this.sizes.Add(size);
			}

			this.seatsCount = seatsSum;
		}

		public string ToName(int index) {
			var coord = indexToCoord(index);
			var width = WidthForRow(coord.z);
			return WidthsNaming.widthsNaming[width][coord.x] + "" + (coord.z + 1);
		}

		internal int? FromName(string t) {
			try {
				var z = int.Parse(t.Substring(1)) - 1;
				var width = WidthForRow(z);
				var naming = WidthsNaming.widthsNaming[width];
				var x = 0;
				for(; x < naming.Length; x++) if(char.ToLowerInvariant(naming[x]) == char.ToLowerInvariant(t[0])) break;

				return coordToIndex(x, z); 
			}
			catch(Exception ex) {
				return null;
			}
		}
	}

	public struct Seat {
		public byte Class;
		public bool Occupied;
	}

	public static class Occupation {
		public static bool Occupied(byte[] occupation, int size, int i) {
			if(i >= 0 && i < size) return ((occupation[i/8] >> (i%8)) & 1) != 0; 
			else throw new IndexOutOfRangeException();
		}

		public static void Occupy(byte[] occupation, int size, int i) {
			if(i >= 0 && i < size) occupation[i/8] |= (byte) (1u << (i%8)); 
			else throw new IndexOutOfRangeException();
		}
	}

	[Serializable] public struct Seats {
		public SeatsScheme Scheme{ get; private set; }
		private byte[] seatsClasses;
		private byte[] seatsOccupied; 

		public Seats(SeatsScheme scheme, IEnumerable<byte> seatsClasses, IEnumerable<byte> seatsOccupied) {
			this.Scheme = scheme;
			this.seatsOccupied = seatsOccupied.ToArray();
			Debug.Assert(this.seatsOccupied.Length == this.Scheme.SeatsCount / 8 + (this.Scheme.SeatsCount % 8 != 0 ? 1 : 0));
			this.seatsClasses = seatsClasses.ToArray();
			Debug.Assert(this.seatsClasses.Length == this.Scheme.SeatsCount);
		}

		public int Size{ get{ return Scheme.SeatsCount; } }

		public bool Occupied(int i) {
			return Occupation.Occupied(seatsOccupied, Scheme.SeatsCount, i);
		}

		public bool Occupied(int x, int z) { 
			return this.Occupied(Scheme.SeatsBeforeZ(z) + x);
		}

		public byte Class(int i) {
			if(i >= 0 && i < Scheme.SeatsCount) return seatsClasses[i];
			else throw new IndexOutOfRangeException();
		}

		public byte Class(int x, int z) { 
			return this.Class(Scheme.SeatsBeforeZ(z) + x);
		}

		public Seat this[int i] { get{
			return new Seat{ Class = Class(i), Occupied = Occupied(i) };
		} }

		public Seat this[int x, int z] { get{
			return new Seat{ Class = Class(x, z), Occupied = Occupied(x, z) };
		} }

		public IEnumerator<bool> GetOccupationEnumerator() {
			return new OccupiedEnumerator(seatsOccupied, Scheme.SeatsCount);
		}

		public IEnumerator<byte> GetClassEnumerator() {
			return seatsClasses.Cast<byte>().GetEnumerator();
		}

		public IEnumerator<Seat> GetEnumerator() { 
			return new SeatEnumerator(GetClassEnumerator(), GetOccupationEnumerator()); 
		}

		class OccupiedEnumerator : IEnumerator<bool> {
			private byte[] seatsOccupied;
			private int count;
			private int index;

			public OccupiedEnumerator(byte[] seatsOccupied, int count) {
				this.seatsOccupied = seatsOccupied;
				this.count = count;
				this.index = -1;
			}

			public bool Current{
				get{
					if(index >= 0 && index >= count) throw new IndexOutOfRangeException();
					else return ((seatsOccupied[index/8] >> (index%8)) & 1) != 0; 
				}
			}

			object IEnumerator.Current => Current;

			public void Dispose() {}

			public bool MoveNext() {
				if(index >= count) return false;
				else {
					index++;
					return index < count;
				}
			}

			public void Reset() {
				index = 0;
			}
		}

		class SeatEnumerator : IEnumerator<Seat> {
			IEnumerator<byte> classes;
			IEnumerator<bool> occupied;

			public SeatEnumerator(IEnumerator<byte> classes, IEnumerator<bool> occupied) {
				this.classes = classes;
				this.occupied = occupied;
			}

			public Seat Current{ get{ return new Seat{ Class = classes.Current, Occupied = occupied.Current }; } }

			object IEnumerator.Current{ get{ return Current; } }

			public void Dispose() {
				classes.Dispose();
				occupied.Dispose();
			}

			public bool MoveNext() {
				var f = classes.MoveNext();
				var s = occupied.MoveNext();
				Debug.Assert(f == s);
				return f;
			}

			public void Reset() {
				classes.Reset();
				occupied.Reset();
			}
		}

		private Seats(SerializationInfo info, StreamingContext context) {
			Debug.Assert(info.GetInt32("version") == 0);
			this.Scheme = (SeatsScheme) info.GetValue("scheme", typeof(SeatsScheme));
			this.seatsClasses = (byte[]) info.GetValue("seatsClasses", typeof(byte[]));
			this.seatsOccupied = (byte[]) info.GetValue("seatsOccupied", typeof(byte[]));
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("version", 0);
			info.AddValue("scheme", this.Scheme, typeof(SeatsScheme));
			info.AddValue("seatsClasses", this.seatsClasses, typeof(byte[]));
			info.AddValue("seatsOccupied", this.seatsOccupied, typeof(byte[]));
		}
	}

	public static class WidthsNaming {
		public static Dictionary<int, char[]> widthsNaming = new Dictionary<int, char[]>();

		static WidthsNaming() {
			widthsNaming.Add(4, new char[]{ 'A', 'C', 'D', 'F' });
			widthsNaming.Add(6, new char[]{ 'A', 'B', 'C', 'D', 'E', 'F' });
		}
	}
}
