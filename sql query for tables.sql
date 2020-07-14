
USE NewDataBase;

CREATE TABLE Product(
	ProductID INT IDENTITY(1,1) NOT NULL,
	ProductName NVARCHAR(250) NOT NULL,
	Price NUMERIC(10,2) NOT NULL,
	Constraint PK_Product PRIMARY KEY (ProductID)
);

Create Table Customers(
	CustomerID int Identity(1,1) Not NUll,
	FirstName NVARCHAR(250) NOT NULL,
	LastName NVARCHAR(250) NOT NULL,
	Constraint PK_Customer PRIMARY KEY (CustomerID)
);

USE NewDataBase;



CREATE TABLE Location (
	LocationID INT IDENTITY(1,1) NOT NULL,
	LocationName NVARCHAR(250) NOT NULL,
	Constraint PK_Location PRIMARY KEY (LocationID)

);

CREATE TABLE OrderHistory (
	OrderID INT IDENTITY(1,1) NOT NULL,
	CustomerID INT NOT NULL,
	LocationID INT NOT NULL,
	Date DATE NOT NULL,
	Time TIME(7) NOT NULL,
	Constraint PK_OrderHistory PRIMARY KEY (OrderID),
	CONSTRAINT FK_OrderHistory_Customer_CustomerID FOREIGN KEY (CustomerID)
		REFERENCES Customers (CustomerID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_OrderHistory_Location_LocationID FOREIGN KEY (LocationID)
		REFERENCES Location (LocationID) ON DELETE CASCADE ON UPDATE CASCADE,
);

Create Table Orders(
	OrderID int NOT NULL, 
	ProductID int Not NUll,
	Amount Int Check (Amount>= 0) NOT NULL,
	Constraint PK_Orders PRIMARY KEY (OrderID, ProductID),
	CONSTRAINT FK_Orders_OrderHistory_OrderID FOREIGN KEY (OrderID)
		REFERENCES OrderHistory (OrderID) On DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_Orders_Product_ProductID FOREIGN KEY (ProductID)
		REFERENCES Product (ProductID) On DELETE CASCADE ON UPDATE CASCADE
	
);


CREATE TABLE Inventory(
	LocationID INT NOT NULL,
	ProductID INT NOT NULL,
	Amount INT CHECK (Amount >= 0) NOT NULL,
	CONSTRAINT PK_Inventory PRIMARY KEY (LocationID, ProductID),
	CONSTRAINT FK_Inventory_Location_LocationID FOREIGN KEY (LocationID)
		REFERENCES Location (LocationID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT FK_Inventory_Product_ProductID FOREIGN KEY (ProductID)
		REFERENCES Product (ProductID) ON DELETE CASCADE ON UPDATE CASCADE

);

EXEC sp_rename 'Customers', 'Customer'
EXEC sp_rename 'Orders', 'Order'


INSERT INTO Product (ProductName, Price)
VALUES ('Apple', 2.00)

INSERT INTO Product (ProductName, Price)
VALUES ('Orange', 3.00)

INSERT INTO Product (ProductName, Price)
VALUES ('Strawberry', 1.00)

INSERT INTO Product (ProductName, Price)
VALUES ('Peach', 2.50)


INSERT INTO Location (LocationName)
VALUES ('Walmart')

INSERT INTO Location (LocationName)
VALUES ('ShopRite')


INSERT INTO Inventory (LocationID, ProductID, Amount)
VALUES (1,1, 50)

INSERT INTO Inventory (LocationID, ProductID, Amount)
VALUES (1,2, 50)

INSERT INTO Inventory (LocationID, ProductID, Amount)
VALUES (2,2, 50)

INSERT INTO Inventory (LocationID, ProductID, Amount)
VALUES (2,3, 50)

INSERT INTO Inventory (LocationID, ProductID, Amount)
VALUES (2,4, 50)

ALTER TABLE OrderHistory
ADD TotalCost decimal;


INSERT INTO Customer (FirstName, LastName)
VALUES ('John', 'Doe')


INSERT INTO Customer (FirstName, LastName)
VALUES ('Nick','Escalona')

INSERT INTO Customer (FirstName, LastName)
VALUES ('Mohamed', 'Salam')

use NewDataBase;

select * 
from product;