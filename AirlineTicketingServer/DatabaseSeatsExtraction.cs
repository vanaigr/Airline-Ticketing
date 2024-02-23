using Communication;
using FlightsSeats;
using System.Collections;
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
                [aps].[SeatIndex],
                [aps].[Class],
                cast(case when [afs].[Passanger] is not null then 1 else 0 end as bit) as [Occupied]
            from (
                select *
                from [Flights].[AirplanesSeats] as [aps]
                where @FlightAirplane = [aps].[Airplane]
            ) as [aps]

            left join (
                select *
                from [Flights].[AvailableFlightsSeats] as [afs]
                where @AvailableFlight = [afs].[AvailableFlight]
                    and [afs].[CanceledIndex] = 0
            ) as [afs]
            on [aps].[SeatIndex] = [afs].[SeatIndex]
            order by [aps].[SeatIndex] ASC;
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

            Debug.Assert(seatsScheme.SeatsCount == rawSeatsOccupation.Count);

            for(int i = 0; i < seatsScheme.SeatsCount; i++) {
                Debug.Assert(rawSeatsOccupation[i].index == i);
            }

            return new Seats(
                seatsScheme,
                new MappingEnumerator<RawSeat, byte>(rawSeatsOccupation, it => it.classId),
                new MappingEnumerator<RawSeat, bool>(rawSeatsOccupation, it => it.occupied)
            );
        }

        private sealed class MappingEnumerator<T, T2> : IEnumerator<T2> {
            private IEnumerator<T> inner;
            private Map<T, T2> map;

            public delegate T Map<F, T>(F f);

            public MappingEnumerator(IEnumerable<T> it, Map<T, T2> map) {
                this.inner = it.GetEnumerator();
                this.map = map;
            }

            public MappingEnumerator(IEnumerator<T> inner, Map<T, T2> map) {
                this.inner = inner;
                this.map = map;
            }

            public T2 Current => map(inner.Current);

            object IEnumerator.Current => this.Current;

            public void Dispose() { inner.Dispose(); }

            public bool MoveNext() { return inner.MoveNext(); }

            public void Reset() { inner.Reset(); }
        }
    }
}
