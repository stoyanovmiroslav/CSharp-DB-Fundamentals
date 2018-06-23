SELECT i.Name, i.Price, i.MinLevel, 
       s.Strength, s.Defence, s.Speed, s.Luck, s.Mind
FROM [Statistics] AS s
INNER JOIN Items AS i
ON i.StatisticId = s.Id
WHERE s.Luck > (SELECT AVG(s.Luck) AS AverageLuck
FROM [Statistics] AS s) 
AND s.Speed > (SELECT AVG(s.Speed) AS AverageSpeed
FROM [Statistics] AS s)
AND s.Mind > (SELECT AVG(s.Mind) AS AverageSpeed
FROM [Statistics] AS s)
ORDER BY i.Name