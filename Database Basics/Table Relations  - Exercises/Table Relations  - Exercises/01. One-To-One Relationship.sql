CREATE TABLE Passports(
 PassportID     INT,
 PassportNumber NVARCHAR(15),
 CONSTRAINT PK_PassportID PRIMARY KEY(PassportID)
)

CREATE TABLE Persons(
 PersonID   INT,
 FirstName  NVARCHAR(15),
 Salary     DECIMAL(15, 2),
 PassportID INT UNIQUE,
 CONSTRAINT FK_PassportID_Passports FOREIGN KEY(PassportID) REFERENCES Passports(PassportID),
 CONSTRAINT PK_PersonID PRIMARY KEY(PersonID)
)

INSERT INTO Passports VALUES 
(101, 'N34FG21B'),
(102, 'K65LO4R7'),
(103, 'ZE657QP2')

INSERT INTO Persons VALUES
(1, 'Roberto', 43300.00, 102),
(2,'Tom', 56100.00,	103),
(3,'Yana', 60200.00, 101)

