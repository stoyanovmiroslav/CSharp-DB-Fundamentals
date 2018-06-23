CREATE TABLE People(
Id INT PRIMARY KEY IDENTITY,
Name NVARCHAR(200) NOT NULL,
Picture VARBINARY(MAX),
Height DECIMAL(15, 2) NOT NULL,
Weight DECIMAL(15, 2) NOT NULL,
Gender NVARCHAR(1) CHECK(Gender = 'm' or Gender = 'f') NOT NULL,
BirthDay DATETIME2 NOT NULL,
Biography NVARCHAR(MAX)
)

INSERT INTO People Values
('Ivan', NULL, 179.5, 82.5, 'm', CONVERT(DATETIME2, '22-12-1987', 103), NULL),
('Misho', NULL, 176.5, 82.5, 'm', '1986-02-13', NULL),
('Ruslan', NULL, 172.5, 82.5, 'm', '1990-10-12', NULL),
('Bilyana', NULL, 163.5, 62.5, 'f', '1991-03-23', NULL),
('Jordan', NULL, 183.5, 87.5, 'm', '1985-12-13', NULL)

SELECT * FROM People