SELECT TOP (5) e.EmployeeID,
               e.FirstName,
               p.Name
FROM EmployeesProjects AS ep
     JOIN Employees AS e ON e.EmployeeID = ep.EmployeeID
     JOIN Projects AS p ON p.ProjectID = ep.ProjectID
WHERE p.EndDate IS NULL
      AND p.StartDate > '08/13/2002'
ORDER BY e.EmployeeID;
