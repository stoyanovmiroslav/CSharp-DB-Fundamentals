CREATE DATABASE CarRental

USE CarRental

CREATE TABLE Categories(
Id INT PRIMARY KEY IDENTITY,
CategoryName NVARCHAR(30) NOT NULL,
DailyRate DECIMAL(15,2) NOT NULL,
WeeklyRate DECIMAL(15,2),
MonthlyRate DECIMAL(15,2),
WeekendRate DECIMAL(15,2)
)

CREATE TABLE Cars(
Id INT PRIMARY KEY IDENTITY,
PlateNumber NVARCHAR(15) NOT NULL,
Manufacturer NVARCHAR(15) NOT NULL,
Model NVARCHAR(15) NOT NULL,
CarYear NVARCHAR(15) NOT NULL,
CategoryId INT FOREIGN KEY (CategoryId) REFERENCES Categories(Id) NOT NULL,
Doors INT NOT NULL,
Picture VARBINARY ,
Condition NVARCHAR(15) NOT NULL,
Available BIT NOT NULL
)

CREATE TABLE Employees(
Id INT PRIMARY KEY IDENTITY,
FirstName NVARCHAR(15) NOT NULL,
LastName NVARCHAR(15) NOT NULL,
Title NVARCHAR(15) NOT NULL,
Notes NVARCHAR(15)
)

CREATE TABLE Customers(
Id INT PRIMARY KEY IDENTITY,
DriverLicenceNumber NVARCHAR(15) NOT NULL,
FullName NVARCHAR(15) NOT NULL,
Address NVARCHAR(15) NOT NULL,
City NVARCHAR(15) NOT NULL,
ZIPCode NVARCHAR(15),
Notes NVARCHAR(15)
)

CREATE TABLE RentalOrders(
Id INT PRIMARY KEY IDENTITY,
EmployeeId INT FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) NOT NULL,
CustomerId INT FOREIGN KEY (CustomerId) REFERENCES Customers(Id) NOT NULL,
CarId INT FOREIGN KEY (CarId) REFERENCES Cars(Id) NOT NULL,
TankLevel INT NOT NULL,
KilometrageStart INT NOT NULL,
KilometrageEnd INT NOT NULL,
TotalKilometrage AS (KilometrageEnd - KilometrageStart),
StartDate DATETIME2 NOT NULL,
EndDate DATETIME2 NOT NULL,
TotalDays AS DATEDIFF(DAY, StartDate, EndDate),
RateApplied DECIMAL(15, 2),
TaxRate DECIMAL(15, 2),
OrderStatus BIT NOT NULL,
Notes NVARCHAR(30)
)

INSERT INTO Categories VALUES 
('CARS', 10, 9, 9,8),
('BUSES', 10, 9, 9,8),
('VANS', 10, 9, 9,8)

INSERT INTO Cars VALUES 
('BT1254LF', 'Reno', 'Megane', '2016', 2, 4, NULL, 'Perfect' , 1),
('C455GL', 'Mercedes', 'Benz', '2018', 1, 4, NULL, 'Good' , 0),
('A1325VL', 'Opel', 'Astra', '2016', 2, 4, NULL, 'Good' , 1)

INSERT INTO Employees VALUES 
('Ivan', 'Ivanov', 'Manager', NULL),
('Georgi', 'Ivanov', 'Staff Member', NULL),
('Mihail', 'Stoyanov', 'Staff Member', NULL)

INSERT INTO Customers VALUES 
('DN4435343', 'Georgi Ivanov', 'Slaveikov', 'Burgas', '8000', 'SA'),
('DN8765454', 'Milena Stoikova', 'Mladost', 'Sofia', '1111', NULL),
('DN5654734', 'Denica Petkova', 'Students City', 'Sofia', '1111', NULL)

INSERT INTO RentalOrders VALUES 
(1, 1, 2, 60, 23456, 23956, '2018-12-22', '2018-12-24', 10, 20, 0, NULL),
(1, 1, 2, 60, 23456, 23876, '2018-12-20', '2018-12-24', 10, 20, 0, NULL),
(1, 1, 2, 60, 23456, 23756, '2018-12-18', '2018-12-24', 10, 20, 0, NULL)

SELECT * FROM RentalOrders