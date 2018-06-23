SELECT COUNT(*) AS Count 
FROM Employees AS e
WHERE e.ManagerID IS NULL

SELECT COUNT(*) - COUNT(e.ManagerID) AS Count 
FROM Employees AS e
