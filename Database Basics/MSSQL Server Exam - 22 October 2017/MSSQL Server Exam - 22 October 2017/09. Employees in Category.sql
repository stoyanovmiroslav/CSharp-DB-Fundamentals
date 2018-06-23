SELECT c.Name,
       COUNT(*) AS [Employees Number]
FROM Categories AS c
INNER JOIN Employees AS e
ON e.DepartmentId = c.DepartmentId
GROUP BY c.Name
ORDER BY c.Name