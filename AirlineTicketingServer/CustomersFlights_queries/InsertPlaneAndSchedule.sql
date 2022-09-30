begin transaction

insert into [Flights].[Airplanes] ([Name])
values ('Airbus A320');
declare @AirplaneId AS int;
set @AirplaneId = SCOPE_IDENTITY();

insert into [Flights].[AirplanesSeatsCount] ([Airplane], [Class], [SeatCount])
values (
	@AirplaneId, 
	(select [Classes].[Id]
	from [Flights].[Classes] as [Classes]
	where [Classes].[Name] = N'Бизнес'),
	8
);

insert into [Flights].[AirplanesSeatsCount] ([Airplane], [Class], [SeatCount])
values (
	@AirplaneId, 
	(select [Classes].[Id]
	from [Flights].[Classes] as [Classes]
	where [Classes].[Name] = N'Эконом'),
	150
);

insert into [Flights].[PeriodicFlightsSchedule] (
	[AirlineDesignator], [Number],
	[AirplaneID],
    [StartDate], [StartTimeMinutes],
    [DateRepeatPeriodDays], [TimeRepeatPeriodMinutes]
)
values (
	'AL', 993,
	@AirplaneId,
	'2022-09-30', 320, --5:20
	1, 300 --каждые 5 часов
);

commit transaction;