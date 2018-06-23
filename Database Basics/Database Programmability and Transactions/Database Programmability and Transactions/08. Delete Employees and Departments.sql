CREATE PROC usp_DeleteEmployeesFromDepartment (@departmentId INT) 
AS
BEGIN TRAN 
UPDATE Employees
SET ManagerID = NULL 
WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

DELETE EmployeesProjects
WHERE EmployeeID IN (
SELECT e.EmployeeID FROM Employees AS e
WHERE DepartmentID = @departmentId)

ALTER TABLE Departments
ALTER COLUMN ManagerID INT

UPDATE Departments
SET ManagerID = NULL 
WHERE ManagerID IN(SELECT EmployeeID FROM Employees WHERE DepartmentID = @departmentId)

DELETE Employees
WHERE DepartmentID = @departmentId

DELETE Departments
WHERE DepartmentID = @departmentId

SELECT COUNT(*) 
FROM Employees
WHERE DepartmentID = @departmentId
ROLLBACK TRAN
GO

EXEC dbo.usp_DeleteEmployeesFromDepartment 3

SELECT COUNT(*) 
FROM Employees
WHERE DepartmentID = 3