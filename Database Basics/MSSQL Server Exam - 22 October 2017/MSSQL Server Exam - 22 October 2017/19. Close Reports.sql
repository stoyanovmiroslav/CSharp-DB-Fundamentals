CREATE TRIGGER tr_ChangeStatusId ON Reports AFTER UPDATE
AS
     UPDATE Reports
	 SET StatusId = 3
	 FROM inserted AS i
	 WHERE i.CloseDate IS NOT NULL


UPDATE Reports
SET CloseDate = GETDATE()
WHERE EmployeeId = 5;