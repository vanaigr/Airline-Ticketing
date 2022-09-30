create schema Customers;

go 
create table [Customers].[LoginInfo] (
	[CustomerID] int identity(1, 1) not null,
	[Login] char(64) not null,
	[Salt] char(10) not null,
	[PasswordHash] char(256) not null
);