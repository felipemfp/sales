create database Sales

use Sales

create table Manufacturer (
  Id int primary key identity,
  Description varchar(100) not null
)

create table Product (
  Id int primary key identity,
  ManufacturerId int not null foreign key references Manufacturer(Id) on delete cascade,
  Description varchar(100) not null,
  Stock int not null default 0,
  Price money not null
)

create table Client (
  Id int primary key identity,
  Name varchar(100) not null,
  VIP bit not null default 0
)

create table Sale (
  Id int primary key identity,
  ClientId int not null foreign key references Client(Id) on delete cascade,
  DateSale datetime not null default getdate(),
  Total money not null default 0,
  Discount money not null default 0,
  FinalTotal money not null default 0
)

create table SaleProduct (
  SaleId int foreign key references Sale(Id) on delete cascade,
  ItemId int,
  ProductId int not null foreign key references Product(Id) on delete cascade,
  Quantity int not null,
  Price money not null,
  primary key (SaleId, ItemId)
)
