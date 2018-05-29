CREATE DATABASE Hotel

USE Hotel

CREATE TABLE Employees(
Id INT PRIMARY KEY IDENTITY,
FirstName NVARCHAR(15),
LastName NVARCHAR(15),
Title NVARCHAR(15),
Notes NVARCHAR(30)
)

CREATE TABLE Customers(
AccountNumber NVARCHAR(15)PRIMARY KEY,
FirstName NVARCHAR(15),
LastName NVARCHAR(15),
PhoneNumber NVARCHAR(15),
EmergencyName NVARCHAR(15),
EmergencyNumber NVARCHAR(15),
Notes NVARCHAR(15)
)

CREATE TABLE RoomStatus(
RoomStatus NVARCHAR(15) PRIMARY KEY,
Notes NVARCHAR(15) 
)

CREATE TABLE RoomTypes(
RoomType NVARCHAR(15) PRIMARY KEY,
Notes NVARCHAR(15)
)

CREATE TABLE BedTypes(
BedType NVARCHAR(15) PRIMARY KEY,
Notes NVARCHAR(15)
)

CREATE TABLE Rooms(
RoomNumber INT PRIMARY KEY,
RoomType NVARCHAR(15) FOREIGN KEY (RoomType) REFERENCES RoomTypes(RoomType),
BedType NVARCHAR(15) FOREIGN KEY (BedType) REFERENCES BedTypes(BedType),
Rate INT, 
RoomStatus NVARCHAR(15) FOREIGN KEY (RoomStatus) REFERENCES RoomStatus(RoomStatus),
Notes NVARCHAR
)

CREATE TABLE Occupancies(
Id INT PRIMARY KEY IDENTITY, 
EmployeeId INT FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
DateOccupied DATETIME2,
AccountNumber NVARCHAR(15) FOREIGN KEY (AccountNumber) REFERENCES Customers(AccountNumber),
RoomNumber INT FOREIGN KEY (RoomNumber) REFERENCES Rooms(RoomNumber),
RateApplied INT,
PhoneCharge DECIMAL(15, 2),
Notes NVARCHAR
)

CREATE TABLE Payments(
Id INT PRIMARY KEY IDENTITY, 
EmployeeId INT FOREIGN KEY (EmployeeId) REFERENCES Employees(Id),
PaymentDate DATETIME2 NOT NULL,
AccountNumber NVARCHAR(15) NOT NULL,
FirstDateOccupied DATETIME2 NOT NULL, 
LastDateOccupied DATETIME2 NOT NULL,
TotalDays AS DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied),
AmountCharged DECIMAL(15, 2),
TaxRate DECIMAL(15, 2),
TaxAmount AS AmountCharged * TaxRate / 100,
PaymentTotal AS AmountCharged  + (AmountCharged * TaxRate / 100),
Notes NVARCHAR(30)
)

INSERT INTO Payments VALUES
(1, '2018-05-28', 'BG432422332', '2018-06-01' ,'2018-06-05', 50, 10, NULL),
(2, '2018-05-29', 'BG321312321', '2018-06-01' ,'2018-06-05',75, 10, NULL),
(3, '2018-05-30', 'BG213123123', '2018-06-02' ,'2018-06-05', 100, 10, NULL)

SELECT * FROM Payments

INSERT INTO Employees VALUES
('Ivan', 'Ivanov', 'Manager', NULL),
('Georgi', 'Ivanov', 'Crew Member', NULL),
('Miroslav', 'Ivanov', 'Manager', NULL)

INSERT INTO Customers VALUES
('BG213123123', 'Ivan', 'Ivanov', '0887874156', 'Velichka', '0887213213', NULL),
('BG321312321', 'Georgi', 'Ivanov', '0887874156', 'Ivailo', '0886543233', NULL),
('BG432422332', 'Miroslav', 'Ivanov', '0887874156', 'Zdravko', '0821522321', NULL)

INSERT INTO RoomStatus VALUES
('occupied', NULL),
('free', NULL),
('cleaning', NULL)

INSERT INTO RoomTypes VALUES
('apartment', NULL),
('studio', NULL),
('doubleRoom', NULL)

INSERT INTO BedTypes VALUES
('double', NULL),
('single', NULL),
('sofa', NULL)

INSERT INTO Rooms VALUES
(101, 'apartment', 'double', 6, 'free', NULL),
(102, 'studio', 'single', 6, 'free', NULL),
(103, 'apartment', 'double', 6, 'cleaning', NULL)

INSERT INTO Occupancies VALUES
(1, '2018-05-28', 'BG432422332', 101, 6, 37, NULL),
(2, '2018-05-29', 'BG321312321', 102, 5, 0, NULL),
(3, '2018-05-30', 'BG213123123', 103, 6, 17, NULL)