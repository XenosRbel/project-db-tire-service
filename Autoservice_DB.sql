DROP DATABASE   IF     EXISTS Autoservice;
CREATE DATABASE IF NOT EXISTS Autoservice;
USE Autoservice;


CREATE TABLE Armor
(
	id  INTEGER NULL,
	arrivalDate  DATE NOT NULL,
	idCustomer  INTEGER NOT NULL,
	idServices  INTEGER NOT NULL,
	statusA  boolean NOT NULL,
	dateExection  DATE NOT NULL
)
;



ALTER TABLE Armor
	ADD  PRIMARY KEY (id,idCustomer,idServices)
;



CREATE TABLE Customers
(
	idCustomer  INTEGER NULL,
	fioC  TEXT NOT NULL,
	phone  TEXT NOT NULL,
	email  TEXT NULL
)
;



ALTER TABLE Customers
	ADD  PRIMARY KEY (idCustomer)
;



CREATE TABLE Masters
(
	idMaster  INTEGER NULL,
	fioM  TEXT NOT NULL,
	specialization  TEXT NOT NULL,
	phone  TEXT NOT NULL
)
;



ALTER TABLE Masters
	ADD  PRIMARY KEY (idMaster)
;



CREATE TABLE Orders
(
	idOrder  INTEGER NULL,
	idMaster  INTEGER NOT NULL,
	orderDate  DATE NOT NULL,
	idServices  INTEGER NOT NULL,
	idCustomer  INTEGER NOT NULL,
	countO  INTEGER NOT NULL
)
;



ALTER TABLE Orders
	ADD  PRIMARY KEY (idOrder,idMaster,idServices,idCustomer)
;



CREATE TABLE Services
(
	idServices  INTEGER NULL,
	nameService  TEXT NOT NULL,
	radius  INTEGER NOT NULL,
	price  FLOAT NOT NULL,
	photoDetails  LONGBLOB NULL
)
;



ALTER TABLE Services
	ADD  PRIMARY KEY (idServices)
;



CREATE VIEW BriefOrderInfo AS
	SELECT Orders.idOrder,Customers.fioC,Masters.fioM,Services.nameService,Services.price,Orders.countO,Orders.orderDate
		FROM Orders,Customers,Masters,Services
;



CREATE VIEW FullOrderInfo AS
	SELECT Customers.fioC,Masters.fioM,Masters.phone,Orders.orderDate,Orders.countO,Services.nameService,Services.radius,Services.price,Services.photoDetails
		FROM Customers,Masters,Orders,Services
;



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
