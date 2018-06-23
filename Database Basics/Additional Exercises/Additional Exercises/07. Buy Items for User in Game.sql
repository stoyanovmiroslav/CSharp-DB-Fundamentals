UPDATE UsersGames
SET Cash -= (SELECT SUM(i.Price)
             FROM Items AS i
			 WHERE i.Name IN('Blackguard', 'Bottomless Potion of Amplification', 'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin', 'Golden  Gorget of Leoric', 'Hellfire Amulet'))
WHERE GameId = (SELECT g.Id
                FROM Games AS g
				WHERE g.Name = 'Edinburgh')
--AND UserId = (SELECT u.Id 
--              FROM Users AS u 
--	    	  WHERE u.Username = 'Alex')


INSERT INTO UserGameItems VALUES
(51, 221),
(71, 221),
(157, 221),
(184, 221),
(197, 221),
(223, 221)


SELECT u.Username,
       g.Name AS [Name],
	   ug.Cash AS [Cash],
	   i.Name AS [Item Name]
FROM Users AS u
JOIN UsersGames AS ug
ON ug.UserId = u.Id
JOIN Games AS g
ON g.Id = ug.GameId
JOIN UserGameItems AS ugi
ON ugi.UserGameId = ug.Id
JOIN Items AS i
ON i.Id = ugi.ItemId
WHERE g.Name = 'Edinburgh'
ORDER BY i.Name
