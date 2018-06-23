CREATE PROCEDURE usp_PlaceOrder(@jobId INT, @serialNumber VARCHAR(50),@quantity INT) 
AS 
BEGIN
	DECLARE @partId INT = (SELECT PartId FROM Parts WHERE SerialNumber = @serialNumber)
	DECLARE @orderId INT = (SELECT TOP 1 OrderId FROM Orders WHERE JobId = @jobId AND IssueDate IS NULL)

	IF (@quantity <= 0)
	BEGIN
		;THROW 50012, 'Part quantity must be more than zero!', 1
	END

	IF ((SELECT JobId FROM Jobs WHERE JobId = @jobId AND Status = 'Finished') IS NOT NULL)
	BEGIN
		;THROW 50011, 'This job is not active!', 1
	END
	
	IF ((SELECT JobId FROM Jobs WHERE JobId = @jobId) IS NULL)
	BEGIN
		;THROW 50013, 'Job not found!', 1
	END

	IF (@partId IS NULL)
	BEGIN 
	   ;THROW 50014, 'Part not found!', 1
	END

	IF (@orderId IS NULL)
	BEGIN
		INSERT INTO Orders (JobId, IssueDate) VALUES
		(@jobId, NULL)

		DECLARE @id INT = (SELECT TOP 1 OrderId FROM Orders WHERE JobId = @jobId)

		INSERT INTO OrderParts (OrderId, PartId, Quantity) VALUES 
		(@id, @partId, @quantity)
	END
	ELSE 
	BEGIN
		IF ((SELECT PartId FROM OrderParts WHERE OrderId = @orderId AND PartId = @partId) IS NOT NULL)
		BEGIN
			UPDATE OrderParts
			SET Quantity += @quantity
			WHERE OrderId = @orderId AND PartId = @partId
		END
		ELSE
		BEGIN
			INSERT INTO OrderParts (OrderId, PartId, Quantity) VALUES
			(@orderId, @partId, @quantity)
		END
	END
END