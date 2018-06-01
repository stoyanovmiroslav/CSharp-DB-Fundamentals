SELECT FirstName
FROM Employees
WHERE HireDate BETWEEN '1995-01-01' and '2005-12-31' AND DepartmentId IN(3, 10)