SELECT u.Username, 
       g.Name AS [Game],
	   COUNT(ugi.ItemId) AS [Items Count],
       SUM(i.Price) AS [Items Price]
FROM UsersGames AS ug
     INNER JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
     INNER JOIN Items AS i ON i.Id = ugi.ItemId
     INNER JOIN Users AS u ON u.Id = ug.UserId
     INNER JOIN Games AS g ON g.Id = ug.GameId
GROUP BY u.Username, g.Name
HAVING COUNT(ugi.ItemId) >= 10
ORDER BY COUNT(ugi.ItemId) DESC, SUM(i.Price) DESC, u.Username;