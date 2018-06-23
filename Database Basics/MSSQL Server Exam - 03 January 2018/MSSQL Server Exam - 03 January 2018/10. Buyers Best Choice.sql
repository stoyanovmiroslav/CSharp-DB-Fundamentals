SELECT m.Manufacturer, 
       m.Model,
       COUNT(o.VehicleId) AS [TimesOrdered]
FROM Orders AS o
RIGHT JOIN Vehicles AS v
ON v.Id = o.VehicleId
INNER JOIN Models AS m
ON m.Id = v.ModelId
GROUP BY  m.Manufacturer,m.Model
ORDER BY [TimesOrdered] DESC, m.Manufacturer DESC, m.Model ASC