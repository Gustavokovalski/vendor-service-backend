CREATE DATABASE VendorDB;
GO
USE VendorDB;

CREATE TABLE UserRegistration (
    Id UNIQUEIDENTIFIER PRIMARY KEY default NEWID(),
    Email VARCHAR(120) NOT NULL,
    UserPassword VARCHAR(32) NOT NULL,
    UserProfile int NOT NULL
);

CREATE TABLE Product (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Name VARCHAR (50),
    Price decimal(10,2),
	Active BIT NOT NULL
);

CREATE TABLE SalesOrder (
    Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Email VARCHAR(120) NOT NULL,
	PurchaseDate datetime NOT NULL
);

CREATE TABLE ProductOrder (
	Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
    ProductId int NOT NULL,
    OrderId int NOT NULL,
    Quantity int NOT NULL,
	ProductPrice decimal(10,2),
    TotalPrice decimal(10,2),
    FOREIGN KEY (ProductId) REFERENCES Product (Id),
    FOREIGN KEY (OrderId) REFERENCES SalesOrder (Id)
);

INSERT INTO UserRegistration (Email, UserPassword, UserProfile)
VALUES ('gustavokovalski.saporiti@gmail.com', '00654B99C70AA5E3EF456C5B30848E92', 1);

