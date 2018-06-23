WITH CTE_ClientsClass([Names], Class, Count)
AS(
SELECT c.FirstName + ' ' + c.LastName AS [Names],
       m.Class,
	   COUNT(m.Class) AS Count
FROM Orders AS o
JOIN Vehicles AS v
ON v.Id = o.VehicleId
JOIN Models AS m
ON m.Id = v.ModelId
JOIN Clients AS c
ON c.Id = o.ClientId
GROUP BY c.FirstName, c.LastName, m.Class
),

CTE_ClientsMaxCount([Names], MaxCount)
AS(
SELECT cc.Names,
       MAX(cc.Count) AS MaxCount
FROM CTE_ClientsClass AS cc
GROUP BY cc.Names)


SELECT cmc.Names,
       cc.Class
FROM CTE_ClientsMaxCount AS cmc
JOIN CTE_ClientsClass AS cc
ON cc.Names = cmc.Names
WHERE cc.Count = cmc.MaxCount