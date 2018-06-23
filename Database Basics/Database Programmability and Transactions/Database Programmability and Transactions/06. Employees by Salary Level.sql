CREATE PROC usp_EmployeesBySalaryLevel @SalaryLevel NVARCHAR(MAX)
AS
  BEGIN
     SELECT e.FirstName, 
	        e.LastName 
	 FROM Employees AS e
	 WHERE dbo.ufn_GetSalaryLevel(e.Salary) = @SalaryLevel
  END 
GO

EXEC usp_EmployeesBySalaryLevel 'Low'
