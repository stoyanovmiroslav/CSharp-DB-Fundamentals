SELECT TOP (5) e.EmployeeID,
               e.FirstName,
               CASE
                   WHEN p.StartDate > '01/01/2005'
                   THEN NULL
                   ELSE p.Name
               END AS ProjectName
FROM EmployeesProjects AS ep
     JOIN Employees AS e ON e.EmployeeID = ep.EmployeeID
     JOIN Projects AS p ON p.ProjectID = ep.ProjectID
WHERE e.EmployeeID = 24
ORDER BY e.EmployeeID;
