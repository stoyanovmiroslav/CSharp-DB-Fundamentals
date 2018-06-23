SELECT e.FirstName, 
       e.LastName,
	   r.Description,
	   FORMAT(r.OpenDate, 'yyyy-MM-dd')  AS OpenDate
FROM Reports AS r
JOIN Employees AS e
ON e.Id = r.EmployeeId
WHERE r.EmployeeId IS NOT NULL
ORDER BY r.EmployeeId, r.OpenDate, r.Id