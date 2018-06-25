  SELECT r.Id, 
         r.Price,
  	   h.Name,
  	   c.Name 
    FROM Rooms AS r
    JOIN Hotels AS h ON h.Id = r.HotelId
    JOIN Cities AS c ON c.Id = h.CityId
   WHERE r.Type = 'First Class'
ORDER BY r.Price DESC, r.Id