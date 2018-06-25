SELECT CONCAT(a.FirstName + ' ', ISNULL(a.MiddleName + ' ', ''), a.LastName) AS [Full Name],
	  Year(a.BirthDate) as [BirthYear]
FROM Accounts AS a
WHERE a.BirthDate > '1992'
ORDER BY Year(a.BirthDate) DESC, a.FirstName