  SELECT at.TripId,
         CONCAT(a.FirstName + ' ', ISNULL(a.MiddleName + ' ', ''), a.LastName) AS [Full Name],
  	     c.Name AS [From],
  	     ch.Name AS [To],
    CASE 
    WHEN t.CancelDate IS NOT NULL THEN 'Canceled'
    ELSE CAST(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate) AS varchar(15)) + ' days'
  END AS Duration
    FROM AccountsTrips AS at
    JOIN Accounts AS a
      ON a.Id = at.AccountId
    JOIN Trips AS t
      ON t.Id = at.TripId
    JOIN Cities AS c
      ON c.Id = a.CityId
    JOIN Rooms AS r
      ON r.Id = t.RoomId
    JOIN Hotels AS h
      ON h.Id = r.HotelId
    JOIN Cities AS ch
      ON ch.Id = h.CityId
ORDER BY [Full Name], at.TripId