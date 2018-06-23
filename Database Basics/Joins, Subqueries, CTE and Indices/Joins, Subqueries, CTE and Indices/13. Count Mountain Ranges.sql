SELECT mc.CountryCode,
       COUNT(*) AS MountainRanges
FROM Mountains AS m
     JOIN MountainsCountries AS mc ON mc.MountainId = m.Id
WHERE mc.CountryCode IN('BG', 'US', 'RU')
GROUP BY mc.CountryCode;