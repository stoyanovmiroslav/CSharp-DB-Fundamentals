SELECT * FROM Employees

SELECT FirstName, LastName, JobTitle  
FROM Employees

SELECT FirstName + ' ' + LastName AS FullName, JobTitle
FROM Employees

SELECT * FROM Employees
WHERE HireDate = '07-31-1998'

INSERT INTO Projects(Name, StartDate)
VALUES('Introduction to SQL Course', '1/1/2006')

SELECT * FROM Projects
WHERE StartDate = '1/1/2006'

UPDATE Projects
SET EndDate = '2/2/2007'
WHERE StartDate = '1/1/2006'

DELETE FROM Projects
WHERE EndDate = '2/2/2007'

SELECT E.EmployeeID, E.FirstName, E.LastName
FROM Employees AS E

SELECT DISTINCT DepartmentID FROM Employees

SELECT * FROM Employees
WHERE Salary <= 9500

SELECT * FROM Employees
WHERE NOT (DepartmentID = 7 OR DepartmentID = 14) AND (Salary <= 9500)

SELECT * FROM Employees
WHERE Salary >= 20000 AND Salary <= 22000

SELECT * FROM Employees
WHERE Salary BETWEEN 20000 AND 22000

SELECT * FROM Employees
WHERE DepartmentID NOT IN(7 ,14)

SELECT * FROM Employees
WHERE MiddleName IS NULL

SELECT * FROM Employees
ORDER BY HireDate ASC 
GO

CREATE VIEW v_EployeesByDepartment AS
SELECT e.FirstName + ' ' + e.LastName AS [FullName], e.DepartmentID
FROM Employees AS e
GO

SELECT * FROM v_EployeesByDepartment

USE Geography

SELECT * FROM Peaks
GO

CREATE VIEW v_HighestPeak AS
SELECT TOP(1) P.PeakName, P.Elevation
FROM Peaks AS p
ORDER BY P.Elevation DESC
GO

SELECT * FROM v_HighestPeak 