SELECT TOP(3) m.FirstName + ' ' + m.LastName AS [Mechanic],
       COUNT(*) AS Jobs
FROM Mechanics AS m
LEFT JOIN Jobs AS j
ON j.MechanicId = m.MechanicId
WHERE j.Status != 'Finished'
GROUP BY m.FirstName + ' ' + m.LastName, m.MechanicId
HAVING COUNT(*) > 1
ORDER BY COUNT(*) DESC, m.MechanicId