create schema arm
Go

create table arm.Customer(
	CustomerID int identity(1,1) primary key,
	FirstName nvarchar(20) not null,
	LastName nvarchar(20) not null,
);

create table arm.Item(
	Name nvarchar(30) primary key,
	Price money not null,
	Automatic bit not null
);

create table arm.Location(
	Name nvarchar(20) primary key
);

create table arm.Orders(
	OrderID int identity(1,1) primary key,
	Location nvarchar(20) not null foreign key references arm.Location(Name),
	Customer int not null foreign key references arm.Customer(CustomerID),
	Price money not null,
	PurchaseDate datetime2(7) not null default getdate()
);



create table arm.Invoice(
	InvoiceID int identity(1,1) primary key,
	OrderID int not null foreign key references arm.Orders(OrderID),
	Item nvarchar(30)not null foreign key references arm.Item(Name),
	Quantity int not null,
	Price money not null
);

create table arm.Inventory(
	InvID int identity(1,1) primary key,
	Item nvarchar(30) not null foreign key references arm.Item(Name),
	Location nvarchar(20) not null foreign key references arm.Location(Name),
	Quantity int not null
);


drop table arm.Inventory
drop table arm.Invoice
drop table arm.Orders
drop table arm.Location
drop table arm.Item
drop table arm.Customer

alter table arm.Item add Automatic bit


truncate table arm.Invoice
truncate table arm.Orders

insert into arm.Location (Name) values ('Arlington');
insert into arm.Location (Name) values ('Dallas');
insert into arm.Location (Name) values ('Fort Worth');

insert into arm.Item (Name, Price) values ('S&W M500', 1199);
insert into arm.Item (Name, Price) values ('Remington 870', 399);
insert into arm.Item (Name, Price) values ('Colt Python', 2299);
insert into arm.Item (Name, Price) values ('Glock 19', 499);
insert into arm.Item (Name, Price) values ('Ruger LCP', 299);
insert into arm.Item (Name, Price) values ('COP Derringer', 799);
insert into arm.Item (Name, Price) values ('SPAS 12', 1499);
insert into arm.Item (Name, Price) values ('Ruger SR1911', 799);
insert into arm.Item (Name, Price) values ('Desert Eagle', 1199);
insert into arm.Item (Name, Price) values ('MP5K', 1249);
insert into arm.Item (Name, Price) values ('Calico M100', 1199);
insert into arm.Item (Name, Price) values ('Chiappa Rhino', 1199);
insert into arm.Item (Name, Price, Automatic) values ('FN P900', 1199, 'true');

update arm.Item set automatic = 'false' where Name = 'Desert Eagle'

select * from arm.Inventory
select * from arm.Orders
select * from arm.Invoice
select * from arm.Customer
select * from arm.Item
select * from arm.Location

select * from arm.Invoice where Item = 'Ruger SR1911'
update arm.Invoice set Item = 'Ruger SR1911' where Item = 'Ruger sr1911'




insert into arm.Customer (FirstName, LastName) values ('Bill', 'Jobs');
insert into arm.Customer (FirstName, LastName) values ('Gordon', 'Croft');
insert into arm.Customer (FirstName, LastName) values ('Steve', 'Gates');
insert into arm.Customer (FirstName, LastName) values ('Lara', 'Freeman');


select quantity from arm.inventory where location = 'Arlington' and item = 'S&W M500';

insert into arm.Inventory (Item, Location, quantity) values ('S&W M500', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Remington 870', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Colt Python', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Glock 19', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Ruger LCP', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('COP Derringer', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('SPAS 12', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Ruger SR1911', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Desert Eagle', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('MP5K', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('FN P900', 'Arlington', 10);
insert into arm.Inventory (Item, Location, quantity) values ('S&W M500', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Remington 870', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Colt Python', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Glock 19', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Ruger LCP', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('COP Derringer', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('SPAS 12', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Ruger SR1911', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Desert Eagle', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('MP5K', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('FN P900', 'Dallas', 10);
insert into arm.Inventory (Item, Location, quantity) values ('S&W M500', 'Fort Worth', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Remington 870', 'Fort Worth', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Colt Python', 'Fort Worth', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Glock 19', 'Fort Worth', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Ruger LCP', 'Fort Worth', 10);
insert into arm.Inventory (Item, Location, quantity) values ('COP Derringer', 'Fort Worth', 10);
insert into arm.Inventory (Item, Location, quantity) values ('SPAS 12', 'Fort Worth', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Ruger SR1911', 'Fort Worth', 10);
insert into arm.Inventory (Item, Location, quantity) values ('Desert Eagle', 'Fort Worth', 10);
insert into arm.Inventory (Item, Location, quantity) values ('MP5K', 'Fort Worth', 10);
insert into arm.Inventory (Item, Location, quantity) values ('FN P900', 'Fort Worth', 10);



update arm.inventory set quantity = 10;

update arm.item set Name = 'Ruger SR1911' where Name = 'Ruger sr1911'

 select * from arm.Orders left join arm.Invoice on Orders.OrderID = Invoice.OrderID
 where Invoice.OrderID is null


 delete from arm.Orders
 where OrderID not in (select OrderID from arm.Invoice)

 select * from arm.Orders
 where OrderID not in (select OrderID from arm.Invoice) 

 select * from arm.Invoice
 select * from arm.Orders