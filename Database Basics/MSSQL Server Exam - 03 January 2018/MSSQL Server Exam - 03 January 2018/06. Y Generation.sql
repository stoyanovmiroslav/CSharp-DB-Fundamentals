SELECT c.FirstName,
       c.LastName
FROM Clients AS c
WHERE YEAR(c.BirthDate) BETWEEN 1977 AND 1994
ORDER BY c.FirstName, c.LastName, c.Id