SELECT ISNULL(SUM(p.Price * op.Quantity), 0) AS [Parts Total]
FROM Orders AS o
LEFT JOIN OrderParts AS op
ON op.OrderId = o.OrderId
JOIN Parts AS p
ON p.PartId = op.PartId
WHERE o.IssueDate BETWEEN DATEADD(WEEK,-3, '24 April 2017') AND '24 April 2017'
