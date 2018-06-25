WITH CTE_TravelersByCountries(Id, Email, CountryCode, Trips, RowNumber)
AS(SELECT a.Id, 
        a.Email,
	    c.CountryCode,
	    COUNT(*) AS Trips,
	    ROW_NUMBER() OVER (PARTITION BY c.CountryCode ORDER BY COUNT(*) DESC) AS RowNumber
FROM Cities AS c
JOIN Hotels AS h
ON h.CityId = c.Id
JOIN Rooms AS r
ON r.HotelId = h.Id
JOIN Trips AS t
ON t.RoomId = r.Id
JOIN AccountsTrips AS at
ON at.TripId = t.Id
JOIN Accounts AS a
ON a.Id = at.AccountId
GROUP BY c.CountryCode, a.Id, a.Email)

SELECT t.Id, t.Email, t.CountryCode, t.Trips
FROM CTE_TravelersByCountries AS t
WHERE t.RowNumber = 1
ORDER BY t.Trips DESC, t.Id