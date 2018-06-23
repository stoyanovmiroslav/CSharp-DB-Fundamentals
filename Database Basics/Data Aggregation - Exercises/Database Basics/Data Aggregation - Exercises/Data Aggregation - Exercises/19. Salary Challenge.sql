SELECT TOP(10) e.FirstName, e.LastName, e.DepartmentID
FROM Employees AS e
WHERE e.Salary > (SELECT AVG(Salary) FROM Employees WHERE e.DepartmentID = DepartmentID)
ORDER BY DepartmentID