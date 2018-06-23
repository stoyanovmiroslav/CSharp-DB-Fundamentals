WITH CTE_VehicelLastStatement(Rank, VehicleId, CollectionDate, ReturnDate, CollectionOfficeId, ReturnOfficeId)
AS(
SELECT DENSE_RANK() OVER   
    (PARTITION BY o.VehicleId ORDER BY o.CollectionDate DESC) AS Rank,
	o.VehicleId,
	o.CollectionDate,
	o.ReturnDate,
	o.CollectionOfficeId,
	o.ReturnOfficeId
FROM Orders AS o)

SELECT  m.Manufacturer + ' - ' + m.Model  AS Vehicle,
  CASE
      WHEN Rank IS NULL THEN 'home'
	  WHEN vs.ReturnDate IS NULL THEN 'on a rent'
	  WHEN vs.CollectionOfficeId != vs.ReturnOfficeId THEN CONCAT(t.Name, ' - ', o.Name)
  END AS [Location]
FROM CTE_VehicelLastStatement AS vs
RIGHT JOIN Vehicles AS v
ON v.Id = vs.VehicleId
JOIN Models AS m
ON m.Id = v.ModelId
LEFT JOIN Offices AS o
ON o.Id = vs.ReturnOfficeId
LEFT JOIN Towns AS t
ON t.Id = o.TownId
WHERE vs.Rank = 1 OR vs.Rank IS NULL
ORDER BY m.Manufacturer + ' - ' + m.Model, v.Id