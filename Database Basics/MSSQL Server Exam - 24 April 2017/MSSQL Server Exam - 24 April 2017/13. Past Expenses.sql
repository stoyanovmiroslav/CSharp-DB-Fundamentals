SELECT j.JobId,
       ISNULL(SUM(p.Price * op.Quantity), 0) AS Total
FROM Jobs AS j
LEFT JOIN Orders AS o
ON o.JobId = j.JobId
LEFT JOIN OrderParts AS op
ON op.OrderId = o.OrderId
LEFT JOIN Parts AS p
ON p.PartId = op.PartId
WHERE j.Status = 'finished'
GROUP BY j.JobId
ORDER BY ISNULL(SUM(p.Price * op.Quantity), 0) DESC, j.JobId