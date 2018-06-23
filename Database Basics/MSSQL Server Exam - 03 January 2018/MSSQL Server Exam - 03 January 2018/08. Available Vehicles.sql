SELECT m.Model, 
       m.Seats,
	   v.Mileage
FROM Vehicles AS v
INNER JOIN Models AS m
ON m.Id = v.ModelId
WHERE v.Id NOT IN (SELECT o.VehicleId
             FROM Orders AS o
             WHERE o.CollectionDate IS NOT NULL AND o.ReturnDate IS NULL)
ORDER BY v.Mileage ASC, m.Seats DESC, v.ModelId ASC