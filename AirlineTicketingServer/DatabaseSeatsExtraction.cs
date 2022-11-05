using Communication;
using FlightsSeats;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Server {
	public class DatabaseSeatsExtraction {
		private List<RawSeat> rawSeatsOccupation;
		private byte[] seatsSchemeBin;

		private struct RawSeat {
			public short index;
			public byte classId;
			public bool occupied;
		};

		public static readonly string commandText = @"
			--get seats scheme
			select top 1 [ap].[SeatsScheme]
			from (
				select top 1 [af].[FlightInfo]
				from [Flights].[AvailableFlights] as [af]
				where @AvailableFlight = [af].[Id]
			) as [afFlightInfo]
			
			inner join [Flights].[FlightInfo] as [fi]
			on [afFlightInfo].[FlightInfo] = [fi].[Id]

			inner join [Flights].[Airplanes] as [ap]
			on [fi].[Airplane] = [ap].[Id];

			--get current flight airplane
			declare @FlightAirplane int;

			select top 1 @FlightAirplane = [fi].[Airplane]
			from (
				select top 1 *
				from [Flights].[AvailableFlights] as [af]
				where [af].[Id] = @AvailableFlight
			) as [af]
			
			inner join [Flights].[FlightInfo] as [fi]
			on [af].[FlightInfo] = [fi].[Id];


			--get seats occupation
			select 
				[afs].[SeatIndex],
				[aps].[Class], 
				cast(case when [afs].[Passanger] is not null then 1 else 0 end as bit) as [Occupied]
			from (
				select *
				from [Flights].[AvailableFlightsSeats] as [afs]
				where @AvailableFlight = [afs].[AvailableFlight]
					and [afs].[CanceledIndex] = 0
			) as [afs]

			inner join [Flights].[AirplanesSeats] as [aps]
			on @FlightAirplane = [aps].[Airplane] and [afs].[SeatIndex] = [aps].[SeatIndex]
			order by [afs].[SeatIndex] ASC;
		";

		public DatabaseSeatsExtraction() {				
			rawSeatsOccupation = new List<RawSeat>();
		}

		public Either<Success, InputError> execute(SqlDataReader result) {
			if(!result.Read()) return Either<Success, InputError>.Failure(new InputError(
				"Данный рейс не существует"
			));
			this.seatsSchemeBin = (byte[]) result[0];

			Common.Debug2.AssertPersistent(result.NextResult());

			while(result.Read()) this.rawSeatsOccupation.Add(new RawSeat{
				index = (short) result[0],
				classId = (byte) result[1],
				occupied = (bool) result[2]
			});

			return Either<Success, InputError>.Success(new Success());
		}

		public Seats calculate() {
			var seatsScheme = DatabaseSeats.fromBytes(seatsSchemeBin);

			Common.Debug2.AssertPersistent(seatsScheme.SeatsCount == rawSeatsOccupation.Count);

			var classes = new byte[seatsScheme.SeatsCount];
			for(int i = 0; i < classes.Length; i++) {
				Common.Debug2.AssertPersistent(rawSeatsOccupation[i].index == i);
				classes[i] = rawSeatsOccupation[i].classId;
			}

			var occupation = new bool[seatsScheme.SeatsCount];
			for(int i = 0; i < occupation.Length; i++) {
				Common.Debug2.AssertPersistent(rawSeatsOccupation[i].index == i);
				occupation[i] = rawSeatsOccupation[i].occupied;
			}

			return new Seats(
				seatsScheme, ((IEnumerable<byte>) classes).GetEnumerator(), 
				((IEnumerable<bool>) occupation).GetEnumerator()
			);
		}
	}
}
