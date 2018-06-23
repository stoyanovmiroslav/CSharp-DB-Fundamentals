CREATE OR ALTER PROC usp_TransferMoney(@SenderId INT, @ReceiverId INT, @Amount DECIMAL(16,4))
AS
BEGIN
	DECLARE @SenderAount DECIMAL = (SELECT a.Balance
								FROM Accounts AS a
								WHERE a.Id = @SenderId)
	BEGIN TRAN;
	EXEC usp_WithdrawMoney @SenderId, @Amount
	EXEC usp_DepositMoney @ReceiverId, @Amount

	IF(@SenderAount = (SELECT a.Balance
							FROM Accounts AS a
							WHERE a.Id = @SenderId))
	BEGIN 
		ROLLBACK;
		THROW 99001, 'Cannot complete operation', 1;
		RETURN;
	END
	COMMIT;
END
	
EXEC usp_TransferMoney 1 , 2 , 10
SELECT * FROM Accounts
