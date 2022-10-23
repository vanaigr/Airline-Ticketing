create function [Flights].[GetAvailableFlightsSeats](
	@AvailableFlight int
)
returns @returntable table(
	[SeatsAndClasses] [Flights].[SeatsScheme] not null,
	[Occupation] [Flights].[SeatsOccupation] not null
)
as begin
	declare @SeatsAndClasses [Flights].[SeatsScheme];
	declare @SeatsCount int;

	select 
		@SeatsAndClasses = [ap].[SeatsScheme],
		@SeatsCount = [ap].[EconomyClassSeatsCount] + [ap].[ComfortClassSeatsCount]
					+ [ap].[BusinessClassSeatsCount] + [ap].[FirstClassSeatsCount]
	from (
		select top 1 [af].[FlightInfo]
		from [Flights].[AvailableFlights] as [af]
		where [af].[Id] = @AvailableFlight
	) as [af]

	inner join [Flights].[FlightsInfo] as [fi]
	on [af].[FlightInfo] = [fi].[Id]

	inner join [Flights].[Airplanes] as [ap]
	on [fi].[Airplane] = [ap].[Id];

	declare OccupiedSeats cursor forward_only read_only local
	for (
		select [SeatIndex] 
		from [Flights].[AvailableFlightsSeats] as [afs]
		where [afs].[AvailableFlight] = @AvailableFlight
	);
	open OccupiedSeats;

	declare @Occupation [Flights].[SeatsOccupation];
	set @Occupation = cast(replicate(cast(cast(0 as binary(1)) as char(1)), @SeatsCount/8 + 1) as varbinary(125));

	declare @SeatIndex int;

	fetch next from OccupiedSeats into @SeatIndex;

	while @@FETCH_STATUS = 0 begin
	declare @Byte tinyInt;
	set @Byte = cast(cast(substring(@Occupation, 1 + @SeatIndex/8, 1) as binary(1)) as tinyInt);
	set @Byte |= power(2, (@SeatIndex % 8));

	set @Occupation = stuff(@Occupation, 1+ @SeatIndex/8, 1, cast(cast(@Byte as binary(1)) as char(1)));
	fetch next from OccupiedSeats into @SeatIndex;
	end;

	CLOSE OccupiedSeats;
    DEALLOCATE OccupiedSeats;


end
