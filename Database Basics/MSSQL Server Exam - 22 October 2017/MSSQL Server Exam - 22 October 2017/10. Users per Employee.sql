SELECT e.FirstName + ' ' + e.LastName AS [Name],
       COUNT(r.UserId) as [Users Number]
FROM Reports AS r
RIGHT JOIN Employees as e
ON e.Id = r.EmployeeId
GROUP BY e.FirstName, e.LastName
ORDER BY [Users Number] DESC, [Name]

