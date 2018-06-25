WITH CTE_LuggageFees(TripId, Luggage)
AS(
  SELECT at.TripId,
         SUM(at.Luggage) AS Luggage
    FROM AccountsTrips AS at
   WHERE at.Luggage > 0
GROUP BY at.TripId)

  SELECT t.TripId, 
         t.Luggage,
  	CASE
    WHEN t.Luggage > 5 THEN CONCAT('$', t.Luggage * 5)
    ELSE '$0'
  END AS Luggage
    FROM CTE_LuggageFees AS t
ORDER BY t.Luggage DESC