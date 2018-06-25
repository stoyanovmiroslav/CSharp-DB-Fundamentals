 SELECT  a.Id,
         a.FirstName + ' ' + a.LastName AS [FullName], 
         MAX(DATEDIFF(DAY, ArrivalDate , ReturnDate)) AS [LongestTrip], 
         MIN(DATEDIFF(DAY, ArrivalDate , ReturnDate)) AS [ShortestTrip]
    FROM Trips AS t
    JOIN AccountsTrips AS at ON at.TripId = t.Id
    JOIN Accounts AS a ON a.Id = at.AccountId
   WHERE a.MiddleName IS NULL
GROUP BY a.FirstName, a.LastName, a.MiddleName, a.Id 
ORDER BY [LongestTrip] DESC, a.Id