CREATE TABLE Armor
(
	id  INTEGER primary key IDENTITY,
	arrivalDate  DATE NOT NULL,
	idCustomer  INTEGER NOT NULL,
	idServices  INTEGER NOT NULL,
	statusA  TINYINT NOT NULL,
	dateExecution  DATE NOT NULL
)

CREATE TABLE Customers
(
	idCustomer  INTEGER primary key IDENTITY,
	fioC  varchar(max) NOT NULL,
	phone  varchar(max) NOT NULL,
	email  varchar(max) NULL
)


CREATE TABLE Masters
(
	idMaster  INTEGER primary key IDENTITY,
	fioM  varchar(max) NOT NULL,
	specialization  varchar(max) NOT NULL,
	phone  varchar(max) NOT NULL
)


CREATE TABLE Orders
(
	idOrder  INTEGER primary key IDENTITY,
	idMaster  INTEGER NOT NULL,
	orderDate  DATE NOT NULL,
	idServices  INTEGER NOT NULL,
	idCustomer  INTEGER NOT NULL,
	countO  INTEGER NOT NULL
)


CREATE TABLE Services
(
	idServices  INTEGER primary key IDENTITY,
	nameService  varchar(max) NOT NULL,
	radius  INTEGER NOT NULL,
	price  FLOAT NOT NULL,
	photoDetails  IMAGE NULL
)
GO

CREATE VIEW BriefOrderInfo AS
	SELECT Orders.idOrder,Customers.fioC,Masters.fioM,Services.nameService,Services.price,Orders.countO,Orders.orderDate
		FROM Orders,Customers,Masters,Services
;
GO

CREATE VIEW FullOrderInfo AS
	SELECT Customers.fioC,Masters.fioM,Masters.phone,Orders.orderDate,Orders.countO,Services.nameService,Services.radius,Services.price,Services.photoDetails
		FROM Customers,Masters,Orders,Services
;
GO
create view SelectOrder as (select idOrder, fioM, nameService, fioC, orderDate, countO from Orders
	inner join Customers on Orders.idCustomer = Customers.idCustomer
    inner join Masters on Orders.idMaster = Masters.idMaster
    inner join Services on Orders.idServices = Services.idServices)
;
GO

ALTER TABLE Armor
	ADD CONSTRAINT R_1 FOREIGN KEY (idCustomer) REFERENCES Customers(idCustomer)
;


ALTER TABLE Armor
	ADD CONSTRAINT R_13 FOREIGN KEY (idServices) REFERENCES Services(idServices)
;



ALTER TABLE Orders
	ADD CONSTRAINT R_2 FOREIGN KEY (idMaster) REFERENCES Masters(idMaster)
;


ALTER TABLE Orders
	ADD CONSTRAINT R_3 FOREIGN KEY (idServices) REFERENCES Services(idServices)
;


ALTER TABLE Orders
	ADD CONSTRAINT R_4 FOREIGN KEY (idCustomer) REFERENCES Customers(idCustomer)
;
GO

create procedure Add_Customers(
	@username varchar(max), 
	@phone varchar(max), 
	@email varchar(max)
	)
AS
begin
	insert into Customers values(@username, @phone, @email);
end;
GO

create procedure Add_Masters(@username varchar(max), @spec varchar(max), @phone varchar(max))AS
begin
	insert into Masters values(@username, @spec, @phone);
end;
GO

create procedure Add_Services(@serviceName varchar(max), @rad integer, @priceService float, @photo image) as
begin
	insert into Services values(@serviceName, @rad, @priceService, @photo);
end;
GO

create procedure Add_Armor(@arrival date, @customer varchar(max), @serviceName varchar(max), @stat TINYINT, @execut date) as
begin
	declare @_idServices int;
	DECLARE @_idCustomer int;
    set @_idServices = (select idServices from Services where nameService = @serviceName);
    set @_idCustomer = (select idCustomer from Customers where fioC = @customer);
    
	insert into Armor values(@arrival, @_idCustomer, @_idServices, @stat, @execut);
end;
GO

create procedure Add_Orders(
	@_userMaster varchar(max), 
	@_orderDate date, 
	@_namseService varchar(max), 
	@_userCustomer varchar(max),
    @_count integer) AS
begin
	declare @_idMaster int;
	DECLARE @_idServices int;
	DECLARE @_idCustomer int;
    
    set @_idServices = (select idServices from Services where nameService = @_namseService);
    set @_idCustomer = (select idCustomer from Customers where fioC = @_userCustomer);
    set @_idMaster = (select idMaster from Masters where fioM = @_userMaster);
    
	insert into Orders values(@_idMaster, @_orderDate, @_idServices, @_idCustomer, @_count);
end;
GO