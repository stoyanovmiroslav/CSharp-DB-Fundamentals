CREATE OR ALTER PROC usp_WithdrawMoney (@AccountId  INT, @MoneyAmount DECIMAL(16, 4))
AS
    BEGIN TRAN;
        IF(@MoneyAmount <= 0)
            BEGIN
			ROLLBACK;
			 THROW 99001, 'Not allow negative amount', 1;
			RETURN
			END
                UPDATE Accounts
                  SET
                      Balance -= @MoneyAmount
                WHERE Id = @AccountId;
				IF((SELECT a.Balance FROM Accounts AS a WHERE Id = @AccountId) < 0)
				BEGIN
				 	ROLLBACK;
		            THROW 99001, 'Not enough funds', 1;
			        RETURN
				END
				COMMIT