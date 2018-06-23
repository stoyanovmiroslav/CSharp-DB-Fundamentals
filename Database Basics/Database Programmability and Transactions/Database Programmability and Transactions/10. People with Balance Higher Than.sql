CREATE PROC usp_GetHoldersWithBalanceHigherThan @Salary DECIMAL(15, 4)
AS 
  SELECT ah.FirstName,
         ah.LastName
  FROM(
       SELECT a.AccountHolderId,
       SUM(a.Balance) AS TotalBalace
       FROM Accounts AS a
       GROUP BY AccountHolderId) AS tb
INNER JOIN AccountHolders AS ah
ON ah.Id = tb.AccountHolderId
WHERE tb.TotalBalace > @Salary
ORDER BY ah.LastName, ah.FirstName

EXEC usp_GetHoldersWithBalanceHigherThan 12000

select * from AccountHolders