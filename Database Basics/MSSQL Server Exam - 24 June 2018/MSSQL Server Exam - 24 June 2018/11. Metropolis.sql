SELECT TOP(5) c.Id,
              c.Name AS [City],
			  c.CountryCode AS [Country],
			  Count(a.Id) AS [Accounts]
         FROM Cities AS c
         LEFT JOIN Accounts AS a
           ON a.CityId = c.Id
     GROUP BY c.Id, c.Name, c.CountryCode
     ORDER BY [Accounts] DESC