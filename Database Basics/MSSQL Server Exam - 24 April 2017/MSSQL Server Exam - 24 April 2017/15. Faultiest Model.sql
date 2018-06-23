WITH CTE_ModelTimesServiced(ModelId, [Model], [Times Serviced], [Rank])
AS(
SELECT m.ModelId,
       m.Name AS [Model],
	   COUNT(*) AS [Times Serviced],
	   DENSE_RANK() OVER   
       (ORDER BY  COUNT(*)  DESC) AS Rank 
FROM Models AS m
JOIN Jobs AS j
ON j.ModelId = m.ModelId
GROUP BY m.ModelId, m.Name), 


CTE_ModelPartsTotal(ModelId, [Parts Total])
AS(
SELECT m.ModelId,
	   ISNULL(SUM(p.Price * op.Quantity), 0) AS [Parts Total]
FROM Models AS m
LEFT JOIN Jobs AS j
ON j.ModelId = m.ModelId
LEFT JOIN Orders AS o
ON o.JobId = j.JobId
LEFT JOIN OrderParts AS op
ON op.OrderId = o.OrderId
LEFT JOIN Parts AS p
ON p.PartId = op.PartId
GROUP BY m.ModelId
)

SELECT s.Model,
       s.[Times Serviced],
	   p.[Parts Total]
FROM CTE_ModelTimesServiced AS s
JOIN CTE_ModelPartsTotal AS p
ON p.ModelId = s.ModelId
WHERE s.Rank = 1