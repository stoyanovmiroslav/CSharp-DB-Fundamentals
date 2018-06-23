SELECT e.FirstName + ' ' + e.LastName AS [Name],
      CONCAT(COUNT(r.CloseDate), '/',
	         COUNT(CASE WHEN r.OpenDate  > '01/01/2016' THEN 1 ELSE NULL END)) AS [Closed Open Reports]
FROM Reports AS r
JOIN Employees AS e
ON e.Id = r.EmployeeId
WHERE YEAR(r.CloseDate) = 2016 OR YEAR(r.OpenDate) = 2016 
GROUP BY e.FirstName, e.LastName
ORDER BY Name