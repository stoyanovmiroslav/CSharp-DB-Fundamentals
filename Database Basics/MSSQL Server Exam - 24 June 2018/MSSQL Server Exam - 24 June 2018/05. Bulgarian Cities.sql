SELECT c.Id, c.Name
FROM Cities AS c
WHERE c.CountryCode = 'BG'
ORDER BY c.Name