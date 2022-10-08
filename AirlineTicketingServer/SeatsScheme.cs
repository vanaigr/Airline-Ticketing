using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SeatsScheme {
	[Serializable] public struct SeatStatus {
		internal byte classIndex;
		internal bool occupied;
		public int Class{ get{ return classIndex; } set{ Debug.Assert((byte)value == value); classIndex = (byte) value; } }
		public bool Occupied{ get { return occupied; } set{ occupied = value; } }
	}

	[Serializable] public class Seats {
		List<SeatStatus> seats;
		int width, length;

		public Seats(
			SeatStatus[] seats,
			int width
		) {
			Debug.Assert(seats.Length % width == 0);
			this.width = width;
			this.length = seats.Length / width;
			this.seats = new List<SeatStatus>(seats);
		}

		public SeatStatus this[int x, int z] { get { return seats[x + width*z]; } set { seats[x + width*z] = value; } }

		public int Width { get{ return width; } }
		public int Length { get{ return length; } }

		public IEnumerator<SeatStatus> GetEnumerator() {
			return seats.GetEnumerator();
		}
	}
}
