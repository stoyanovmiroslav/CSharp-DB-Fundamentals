WITH CTE_PartsByMechanicAndVendor(MechanicId, [Mechanic Full Name], [Vendor], [Parts])
AS(
SELECT m.MechanicId,
       m.FirstName + ' ' + m.LastName AS [Mechanic Full Name],
       v.Name AS [Vendor] ,
	   SUM(op.Quantity) AS [Parts]
FROM Vendors AS v
JOIN Parts AS p
ON p.VendorId = v.VendorId
JOIN OrderParts AS op
ON op.PartId = p.PartId
JOIN Orders AS o
ON o.OrderId = op.OrderId
JOIN Jobs AS j
ON j.JobId = o.JobId
JOIN Mechanics AS m
ON m.MechanicId = j.MechanicId
GROUP BY m.MechanicId , m.FirstName + ' ' + m.LastName, v.Name),

CTE_PartsByMechanic(MechanicId, PartsTotalQantity)
AS(
SELECT pmv.MechanicId,
       SUM(Parts) AS PartsTotalQantity
FROM CTE_PartsByMechanicAndVendor AS pmv
GROUP BY pmv.MechanicId
)

SELECT pmv.[Mechanic Full Name],
       pmv.Vendor,
	   pmv.Parts,
	   CONCAT(CAST(CAST(pmv.Parts AS DECIMAL) / CAST(pm.PartsTotalQantity AS DECIMAL) * 100 AS INT), '%') AS Preference
FROM CTE_PartsByMechanicAndVendor AS pmv
JOIN CTE_PartsByMechanic AS pm
ON pm.MechanicId = pmv.MechanicId
ORDER BY pmv.[Mechanic Full Name], pmv.Parts DESC, pmv.Vendor