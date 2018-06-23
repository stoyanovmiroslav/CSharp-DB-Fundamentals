WITH CTE_SevenMostOrderedVehicle(Manufacturer, AverageConsumption, [TimesOrdered])
AS(
SELECT TOP(7) m.Manufacturer,
       AVG(m.Consumption) AS AverageConsumption,
       COUNT(o.CollectionDate) AS [TimesOrdered]
FROM Orders AS o
RIGHT JOIN Vehicles AS v
ON v.Id = o.VehicleId
INNER JOIN Models AS m
ON m.Id = v.ModelId
GROUP BY  m.Manufacturer, m.Model
ORDER BY [TimesOrdered] DESC)

SELECT v.Manufacturer, v.AverageConsumption
FROM CTE_SevenMostOrderedVehicle AS v
where v.AverageConsumption BETWEEN 5 and 15
ORDER BY v.Manufacturer, v.AverageConsumption