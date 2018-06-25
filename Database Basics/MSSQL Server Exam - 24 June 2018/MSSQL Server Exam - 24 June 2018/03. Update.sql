UPDATE Rooms
SET Price = Price + (Price * 14 / 100)
WHERE HotelId IN (5, 7, 9)