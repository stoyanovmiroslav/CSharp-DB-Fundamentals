SELECT TOP (5) e.EmployeeID,
               e.JobTitle,
               e.AddressID,
               a.AddressText
FROM employees AS e
     JOIN Addresses AS a ON a.AddressID = e.AddressID
ORDER BY e.AddressId;