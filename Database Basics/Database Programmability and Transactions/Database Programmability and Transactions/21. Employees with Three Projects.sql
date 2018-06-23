CREATE PROC usp_AssignProject(@emloyeeId INT, @projectID INT)
AS
  BEGIN TRAN
     INSERT INTO EmployeesProjects VALUES
	 (@emloyeeId, @projectID)

     DECLARE @CurrentProjects INT = (SELECT COUNT(*)
                                    FROM EmployeesProjects AS ep
                                   WHERE ep.EmployeeID = @emloyeeId)

         IF (@CurrentProjects > 3)
		 BEGIN
		   ROLLBACK;
		   THROW 99001, 'The employee has too many projects!', 1;
         END
  COMMIT