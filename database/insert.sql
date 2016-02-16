use Sales
go
insert into Manufacturer (Description)
values
  ('Samsung'),
  ('Motorola'),
  ('Nokia'),
  ('Sony'),
  ('Apple')
go
insert into Product (ManufacturerId, Description, Stock, Price)
values
  (1, 'Galaxy S3', 50, 1000.0),
  (1, 'Galaxy S4', 50, 1100.0),
  (1, 'Galaxy S5', 50, 1200.0),
  (1, 'Galaxy S6', 50, 1300.0),
  (1, 'Galaxy J5', 50, 900.0),
  (2, 'Moto G', 50, 600.0),
  (2, 'Mote E', 50, 500.0),
  (2, 'Moto X', 50, 900.0),
  (3, 'Lumia 520', 50, 600.0),
  (3, 'Lumia 620', 50, 650.0),
  (3, 'Lumia 720', 50, 750.0),
  (4, 'Xperia Z3', 50, 1200.0),
  (4, 'Xperia Z5', 50, 1600.0),
  (5, 'Iphone 5s', 50, 1600.0),
  (5, 'Iphone 5c', 50, 1500.0),
  (5, 'Iphone 6s', 50, 2000.0)
go
insert into Client (Name)
values
  ('Gustavo Simpório'),
  ('Jair da Silva'),
  ('Lucas Macedo'),
  ('Monique Galvão'),
  ('João Maria'),
  ('Fabiana Albuquerque'),
  ('Ana Júlia')
