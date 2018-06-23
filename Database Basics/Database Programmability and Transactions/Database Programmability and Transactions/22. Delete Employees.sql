CREATE TABLE Deleted_Employees(
EmployeeId INT PRIMARY KEY IDENTITY,
FirstName NVARCHAR(20), 
LastName NVARCHAR(20),
MiddleName NVARCHAR(20),
JobTitle NVARCHAR(20),
DepartmentId INT,
Salary DECIMAL(15, 2))
GO

CREATE TRIGGER tr_deletedEmployees ON Employees FOR DELETE
AS 
INSERT INTO Deleted_Employees
SELECT d.FirstName, d.LastName, d.MiddleName, d.JobTitle, d.DepartmentID, d.Salary
 FROM deleted AS d