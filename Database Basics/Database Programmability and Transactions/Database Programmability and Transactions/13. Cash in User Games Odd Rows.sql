CREATE OR ALTER FUNCTION ufn_CashInUsersGames (@gameName NVARCHAR(MAX))
RETURNS TABLE
AS
RETURN(
SELECT SUM(t.Cash) AS SumCash
FROM
(
    SELECT g.Name,
           g.Id,
           ug.Cash,
           ROW_NUMBER() OVER(ORDER BY ug.Cash DESC) AS RowNumber
    FROM UsersGames AS ug
         INNER JOIN Games AS g ON g.Id = ug.GameId
    WHERE g.Name = @gameName
) AS t
WHERE t.RowNumber % 2 = 1)
GO

SELECT * FROM ufn_CashInUsersGames('Love in a mist')