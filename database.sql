create database Sales

create table Manufacturer (
  Id int primary key identity,
  Description varchar(100) not null
)

create table Product (
  Id int primary key identity,
  ManufacturerId int foreign key references Manufacturer(Id)
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
  ClientId int foreign key references Client(Id),
  DateSale datetime not null default getdate(),
  Total money not null,
  Discount money not null,
  FinalTotal money not null
)

create table SaleProduct (
  SaleId int foreign key references Sale(Id),
  ProductId int foreign key references Product(Id),
  Quantity int not null,
  Price money not null,
  primary key (SaleId, ProductId)
)
