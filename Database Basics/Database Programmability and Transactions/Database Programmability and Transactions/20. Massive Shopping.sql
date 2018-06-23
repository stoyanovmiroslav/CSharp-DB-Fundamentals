DECLARE @UserID INT = (SELECT u.Id 
                         FROM Users AS u 
						WHERE u.Username = 'Stamat')

DECLARE @GameID INT = (SELECT g.Id 
                         FROM Games AS g 
                        WHERE g.Name = 'Safflower')

DECLARE @UserGameId INT = (SELECT Id 
                             FROM UsersGames 
                            WHERE UserId = @UserId AND GameId = @GameId)

DECLARE @PlayerMoney DECIMAL = (SELECT ug.Cash 
					              FROM UsersGames AS ug
					             WHERE ug.GameId = @GameID AND ug.UserId = @UserID)

DECLARE @ItemsPriceLevel11to12 DECIMAL = (SELECT SUM(i.Price)
                                            FROM items as i
                                           WHERE i.MinLevel BETWEEN 11 AND 12)

DECLARE @ItemsPriceLevel19to21 DECIMAL = (SELECT SUM(i.Price)
                                            FROM items as i
                                           WHERE i.MinLevel BETWEEN 19 and 21)
  

IF(@PlayerMoney >= @ItemsPriceLevel11to12)
    BEGIN
        BEGIN TRAN;
        SET @PlayerMoney-=@ItemsPriceLevel11to12;
        UPDATE UsersGames
          SET
              Cash-=@ItemsPriceLevel11to12
        WHERE GameId = @GameID
              AND UserId = @UserID;
        INSERT INTO UserGameItems
               SELECT i.Id,
                      @UserGameId
               FROM items AS i
               WHERE i.MinLevel BETWEEN 11 AND 12;
        COMMIT;
    END;
	
IF(@PlayerMoney >= @ItemsPriceLevel19to21)
    BEGIN
        BEGIN TRAN;
        SET @PlayerMoney-=@ItemsPriceLevel19to21;
        UPDATE UsersGames
          SET
              Cash-=@ItemsPriceLevel19to21
        WHERE GameId = @GameID
              AND UserId = @UserID;
        INSERT INTO UserGameItems
               SELECT i.Id,
                      @UserGameId
               FROM items AS i
               WHERE i.MinLevel BETWEEN 19 AND 21;
        COMMIT;
    END;

SELECT i.Name AS [Item Name]
FROM UserGameItems AS ugi
     INNER JOIN Items AS i ON i.Id = ugi.ItemId
WHERE UserGameId = @UserGameId
ORDER BY i.Name;