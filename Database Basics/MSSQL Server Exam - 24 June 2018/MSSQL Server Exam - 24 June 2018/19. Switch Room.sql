CREATE PROCEDURE usp_SwitchRoom(@TripId INT, @TargetRoomId INT)
AS
BEGIN
    DECLARE @HotelId INT = (SELECT TOP(1) r.HotelId
	                                 FROM Trips AS t
	                                 JOIN Rooms AS r ON r.Id = t.RoomId
							        WHERE t.Id = @TripId)
      
    DECLARE @TargetRoomHotelId INT = (SELECT TOP(1) r.HotelId
	                                           FROM Rooms AS r
									          WHERE r.Id = @TargetRoomId)            

	   IF(@HotelId != @TargetRoomHotelId)
	    BEGIN 
	      ;THROW 99001, 'Target room is in another hotel!', 1;
	    END

	DECLARE @NumberOfPeople INT = (SELECT COUNT(*)
	                                 FROM AccountsTrips AS at
	                                WHERE at.TripId = @TripId)
  
	   IF((SELECT TOP(1) Beds FROM Rooms WHERE Id = @TargetRoomId) < @NumberOfPeople)
		BEGIN 
		  ;THROW 99001, 'Not enough beds in target room!', 1;
		END

	 UPDATE Trips
	    SET RoomId = @TargetRoomId
	  WHERE Id = @TripId
END