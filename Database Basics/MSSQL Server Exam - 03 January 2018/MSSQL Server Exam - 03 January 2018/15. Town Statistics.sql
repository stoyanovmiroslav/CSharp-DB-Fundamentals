WITH CTE_ClientsByTowns(TownId, Name, Count)
AS(
SELECT t.Id,
       t.Name,
       COUNT(c.Gender) AS Count
FROM Orders AS o
JOIN Clients AS c
ON c.Id = o.ClientId
JOIN Towns AS t
ON t.Id = o.TownId
GROUP BY t.Id, t.Name),

CTE_FemaleByTowns(TownId, Name, Count)
AS(
SELECT t.Id,
       t.Name,
       COUNT(c.Gender) AS Count
FROM Orders AS o
JOIN Clients AS c
ON c.Id = o.ClientId
JOIN Towns AS t
ON t.Id = o.TownId
WHERE c.Gender = 'F'
GROUP BY t.Id, t.Name),

CTE_MaleByTowns(TownId, Name, Count)
AS(
SELECT t.Id,
       t.Name,
       COUNT(c.Gender) AS Count
FROM Orders AS o
JOIN Clients AS c
ON c.Id = o.ClientId
JOIN Towns AS t
ON t.Id = o.TownId
WHERE c.Gender = 'M'
GROUP BY t.Id, t.Name)

SELECT ct.Name,
        CAST(CAST(mt.Count AS DECIMAL) /  CAST(ct.Count AS DECIMAL) * 100 AS INT) AS MalePercent,
		CAST(CAST(ft.Count AS DECIMAL) /  CAST(ct.Count AS DECIMAL) * 100 AS INT) AS FemalePercent
FROM CTE_ClientsByTowns AS ct
LEFT JOIN CTE_FemaleByTowns AS ft
ON ft.TownId = ct.TownId
LEFT JOIN CTE_MaleByTowns AS mt
ON mt.TownId = ct.TownId
ORDER BY ct.Name, ct.TownId