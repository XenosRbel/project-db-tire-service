DROP DATABASE   IF     EXISTS Autoservice;
CREATE DATABASE IF NOT EXISTS Autoservice;
USE Autoservice;


CREATE TABLE Armor
(
	id  INTEGER primary key auto_increment,
	arrivalDate  DATE NOT NULL,
	idCustomer  INTEGER NOT NULL,
	idServices  INTEGER NOT NULL,
	statusA  boolean NOT NULL,
	dateExecution  DATE NOT NULL
)
;

CREATE TABLE Customers
(
	idCustomer  INTEGER primary key auto_increment,
	fioC  TEXT NOT NULL,
	phone  TEXT NOT NULL,
	email  TEXT NULL
)
;

CREATE TABLE Masters
(
	idMaster  INTEGER primary key auto_increment,
	fioM  TEXT NOT NULL,
	specialization  TEXT NOT NULL,
	phone  TEXT NOT NULL
)
;

CREATE TABLE Orders
(
	idOrder  INTEGER primary key auto_increment,
	idMaster  INTEGER NOT NULL,
	orderDate  DATE NOT NULL,
	idServices  INTEGER NOT NULL,
	idCustomer  INTEGER NOT NULL,
	countO  INTEGER NOT NULL
)
;

CREATE TABLE Services
(
	idServices  INTEGER primary key auto_increment,
	nameService  TEXT NOT NULL,
	radius  INTEGER NOT NULL,
	price  FLOAT NOT NULL,
	photoDetails  LONGBLOB NULL
)
;

CREATE VIEW BriefOrderInfo AS
	SELECT Orders.idOrder,Customers.fioC,Masters.fioM,Services.nameService,Services.price,Orders.countO,Orders.orderDate
		FROM Orders,Customers,Masters,Services
;



CREATE VIEW FullOrderInfo AS
	SELECT Customers.fioC,Masters.fioM,Masters.phone,Orders.orderDate,Orders.countO,Services.nameService,Services.radius,Services.price,Services.photoDetails
		FROM Customers,Masters,Orders,Services
;

create view SelectOrder as (select idOrder, fioM, nameService, fioC, orderDate, countO from Orders
	inner join Customers on Orders.idCustomer = Customers.idCustomer
    inner join Masters on Orders.idMaster = Masters.idMaster
    inner join Services on Orders.idServices = Services.idServices);

ALTER TABLE Armor
	ADD FOREIGN KEY R_1 (idCustomer) REFERENCES Customers(idCustomer)
;


ALTER TABLE Armor
	ADD FOREIGN KEY R_13 (idServices) REFERENCES Services(idServices)
;



ALTER TABLE Orders
	ADD FOREIGN KEY R_2 (idMaster) REFERENCES Masters(idMaster)
;


ALTER TABLE Orders
	ADD FOREIGN KEY R_3 (idServices) REFERENCES Services(idServices)
;


ALTER TABLE Orders
	ADD FOREIGN KEY R_4 (idCustomer) REFERENCES Customers(idCustomer)
;

delimiter //
create procedure Add_Customers(username text, phone text, email text)
begin
	insert into Customers values(default, username, phone, email);
end//

create procedure Add_Masters(username text, spec text, phone text)
begin
	insert into Masters values(default, username, spec, phone);
end//

create procedure Add_Services(service text, rad integer, priceService float, photo longblob)
begin
	insert into Services values(default, service, rad, priceService, photo);
end//

create procedure Add_Armor(arrival date, customer text, service text, stat boolean, execut date)
begin
	declare _idServices, _idCustomer int;
    set _idServices = (select idServices from Services where nameService = service);
    set _idCustomer = (select idCustomer from Customers where fioC = customer);
    
	insert into Armor values(default, arrival, _idCustomer, _idServices, stat, execut);
end//

create procedure Add_Orders(
	_userMaster text, 
	_orderDate date, 
	_namseService text, 
	_userCustomer text,
    _count integer)
begin
	declare _idMaster, _idServices, _idCustomer int;
    
    set _idServices = (select idServices from Services where nameService = _namseService);
    set _idCustomer = (select idCustomer from Customers where fioC = _userCustomer);
    set _idMaster = (select idMaster from Masters where fioM = _userMaster);
    
	insert into Orders values(default, _idMaster, _orderDate, _idServices, _idCustomer, _count);
end//
delimiter ;