using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SeatsScheme {
	[Serializable] public struct Point { public int x, z; public Point(int z, int x) { this.x = x; this.z = z; } }

	[Serializable] public struct SeatStatus {
		internal byte classId;
		internal bool occupied;
		public int Class{ get{ return classId; } set{ Debug.Assert((byte)value == value); classId = (byte) value; } }
		public bool Occupied{ get { return occupied; } set{ occupied = value; } }
	}

	[Serializable] public class Seats {
		List<SeatStatus> seats;
		List<int> seatsBeforeZ;
		List<int> sizeIndexForZ;
		List<Point> sizes;
		int totalLength;

		public Seats(
			IEnumerator<SeatStatus> seatsE,
			IEnumerator<Point> sizesE
		) {
			setFromIterators(seatsE, sizesE);
		}

		public SeatStatus this[int x, int z] { 
			get {
				Debug.Assert(x >= 0 && x < WidthForRow(z));
				return seats[seatsBeforeZ[z] + x];
			} 
			set { 
				Debug.Assert(x >= 0 && x < WidthForRow(z));
				seats[seatsBeforeZ[z] + x] = value; 
			} 
		}

		public int WidthForRow(int z) { return sizes[sizeIndexForZ[z]].x; }
		public int TotalLength => totalLength;
		public int SeatsCount => seats.Count;
		public int SizesCount => sizes.Count;

		public Point sizeAtIndex(int i) { return sizes[i]; }

		public SeatStatus seatAtIndex(int i) { return seats[i]; }
		public IEnumerator<SeatStatus> GetSeatsEnumerator() {
			return seats.GetEnumerator();
		}

		public IEnumerator<Point> GetSizesEnumerator() {
			return sizes.GetEnumerator();
		}
		
		protected Seats(SerializationInfo info, StreamingContext context) {
			Debug.Assert(info.GetInt32("version") == 0);
			var sizes = (List<Point>) info.GetValue("sizes", typeof(List<Point>));
			var seats = (List<SeatStatus>) info.GetValue("seats", typeof(List<SeatStatus>));
			setFromIterators(seats.GetEnumerator(), sizes.GetEnumerator());
		}

		public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
			info.AddValue("version", 0);
			info.AddValue("sizes", sizes, typeof(List<Point>));
			info.AddValue("seats", seats, typeof(List<SeatStatus>));
		}

		private void setFromIterators(
			IEnumerator<SeatStatus> seatsE,
			IEnumerator<Point> sizesE
		) {
			this.seatsBeforeZ = new List<int>();
			this.sizeIndexForZ = new List<int>();
			this.sizes = new List<Point>();
			this.seats = new List<SeatStatus>();

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
			while(seatsE.MoveNext()) {
				var seat = seatsE.Current;
				this.seats.Add(seat);
			}
			Debug.Assert(seatsSum == this.seats.Count);
		}
	}
}
