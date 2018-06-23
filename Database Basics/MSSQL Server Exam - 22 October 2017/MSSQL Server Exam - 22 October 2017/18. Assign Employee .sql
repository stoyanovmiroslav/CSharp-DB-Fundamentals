CREATE PROC usp_AssignEmployeeToReport(@employeeId INT, @reportId INT)
AS
   BEGIN TRAN
      UPDATE Reports
      SET EmployeeId = @employeeId
      WHERE Id = @reportId

	  DECLARE @EmployeeDepartmentId INT = (SELECT e.DepartmentId
	                                   FROM Employees AS e
									   WHERE Id = @employeeId)  

	  DECLARE @ReportDepartmentId INT = (SELECT c.DepartmentId
	                                   FROM Reports AS r
									   JOIN Categories AS c
                                       ON c.Id = r.CategoryId
									   WHERE r.Id = @reportId)  

      IF(@EmployeeDepartmentId <> @ReportDepartmentId)
	    BEGIN
	     ROLLBACK;
	     THROW 99001, 'Employee doesn''t belong to the appropriate department!', 1;
	     RETURN
	    END
  COMMIT

EXEC usp_AssignEmployeeToReport 17, 2;
SELECT EmployeeId FROM Reports WHERE id = 2