use OrderDb;
go

create table Orders
(
	ID int identity(1, 1) not null primary key,
	OrderGuid uniqueidentifier not null default newid(),
	Description nvarchar(max) not null,
	Price decimal(10, 2) not null
);
