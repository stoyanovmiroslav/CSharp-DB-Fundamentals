WITH CTE_UndeliveredOrders(Quantity, OrderId, PartId)
AS(
SELECT op.Quantity,
       op.OrderId,
	   op.PartId
FROM Orders AS o
JOIN OrderParts AS op
ON op.OrderId = o.OrderId
WHERE o.Delivered = 0
)

SELECT p.PartId,
       p.Description,
	   pn.Quantity AS [Required],
	   p.StockQty AS [In Stock],
	   ISNULL(uo.Quantity, 0) [Ordered]
FROM PartsNeeded AS pn
JOIN Jobs AS j
ON j.JobId = pn.JobId
JOIN Parts AS p
ON p.PartId = pn.PartId
LEFT JOIN CTE_UndeliveredOrders AS uo
ON uo.PartId = p.PartId
WHERE j.Status != 'finished' AND  p.StockQty  +  ISNULL(uo.Quantity, 0) <  pn.Quantity
ORDER BY p.PartId