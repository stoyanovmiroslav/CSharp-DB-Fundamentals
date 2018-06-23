SELECT TOP (5) c.CountryName,
               r.RiverName
FROM Countries AS c
     LEFT JOIN CountriesRivers AS rc ON c.CountryCode = rc.CountryCode
     LEFT JOIN Rivers AS r ON r.Id = rc.RiverId
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName;