SELECT m.FirstName + ' ' + m.LastName AS Available
FROM Mechanics AS m
WHERE MechanicId NOT IN (SELECT MechanicId
                         FROM Jobs AS j
                         WHERE j.Status != 'finished' AND MechanicId IS NOT NULL
                         GROUP BY MechanicId)
ORDER BY m.MechanicId