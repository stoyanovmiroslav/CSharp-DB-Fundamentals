SELECT c.Name,
      COUNT(h.ID) AS [Hotels]
FROM Cities AS c
LEFT JOIN Hotels AS h
ON h.CityId = c.Id
GROUP BY c.Name
ORDER BY COUNT(h.Id) DESC, c.Name