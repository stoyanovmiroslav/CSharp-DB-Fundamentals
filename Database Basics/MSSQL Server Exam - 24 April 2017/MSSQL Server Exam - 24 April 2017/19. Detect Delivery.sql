CREATE TRIGGER tr_DeliveredOrder ON Orders AFTER UPDATE 
AS
   UPDATE Parts
   SET StockQty += op.Quantity
   FROM Parts as p
   JOIN OrderParts AS op
   ON op.PartId = p.PartId
   JOIN Orders AS o
   ON o.OrderId = op.OrderId
   JOIN inserted AS i
   ON i.OrderId = o.OrderId
   JOIN deleted AS d
   ON d.OrderId = i.OrderId
   WHERE d.Delivered != i.Delivered


UPDATE Orders
SET Delivered = 1
WHERE OrderId = 21