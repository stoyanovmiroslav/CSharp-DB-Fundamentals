CREATE PROC usp_GetEmployeesSalaryAboveNumber @Salary DECIMAL(15, 2)
AS
     BEGIN
	   SELECT e.FirstName,
                e.LastName
         FROM Employees AS e
         WHERE E.Salary >= @Salary
     END;

EXEC usp_GetEmployeesSalaryAboveNumber 48100