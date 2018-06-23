SELECT d.Name,
      ISNULL(CAST(AVG(DATEDIFF(DAY, r.OpenDate, r.CloseDate)) AS VARCHAR), 'no info') AS [Average Duration]
FROM Reports AS r
JOIN Categories AS c
ON c.Id = r.CategoryId
JOIN Departments AS d
ON d.Id = c.DepartmentId
GROUP BY d.Name
ORDER BY d.Name