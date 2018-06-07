CREATE TABLE Manufacturers(
ManufacturerID INT,
[Name] NVARCHAR(15),
EstablishedOn DATETIME2,

CONSTRAINT PK_ManufacturerID PRIMARY KEY (ManufacturerID)
)

CREATE TABLE Models(
ModelID INT,
[Name] NVARCHAR(15),
ManufacturerID INT

CONSTRAINT PK_ModelID PRIMARY KEY (ModelID),
CONSTRAINT FK_ManufacturerID_Manufacturers FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)
)


INSERT INTO Manufacturers VALUES
(1, 'BMW', '07/03/1916'),
(2, 'Tesla', '01/01/2003'),
(3, 'Lada',	'01/05/1966')


INSERT INTO Models VALUES
(101, 'X1',	1),
(102, 'i6',	1),
(103, 'Model S', 2),
(104, 'Model X', 2),
(105, 'Model 3', 2),
(106, 'Nova	', 3)