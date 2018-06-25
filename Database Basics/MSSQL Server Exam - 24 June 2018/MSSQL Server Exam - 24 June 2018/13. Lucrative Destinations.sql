  SELECT TOP(10) c.id,
         c.Name, 
         SUM(BaseRate + r.Price) AS [Total Revenue],
  	     COUNT(t.id) AS [Trips]
    FROM Cities AS c
    JOIN Hotels AS h ON h.CityId = c.Id
    JOIN Rooms AS r ON r.HotelId = h.Id
    JOIN Trips AS t ON t.RoomId = r.Id
   WHERE YEAR(t.BookDate) = '2016'
GROUP BY c.id, c.Name
ORDER BY [Total Revenue] DESC, [Trips] DESC