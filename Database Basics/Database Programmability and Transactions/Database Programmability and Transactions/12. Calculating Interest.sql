CREATE PROC usp_CalculateFutureValueForAccount (@accountId INT, @interestRate FLOAT)
AS 
SELECT a.Id [Account Id],
       ah.FirstName,
	   ah.LastName,
       a.Balance AS [Current Balance], 
       dbo.ufn_CalculateFutureValue(a.Balance, @interestRate, 5) AS [Balance in 5 years]
FROM Accounts AS a
JOIN AccountHolders AS ah
ON ah.Id = a.AccountHolderId
WHERE a.Id = @accountId


EXEC usp_CalculateFutureValueForAccount 1 , 0.1