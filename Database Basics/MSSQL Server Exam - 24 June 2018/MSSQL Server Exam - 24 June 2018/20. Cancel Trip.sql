CREATE TRIGGER tr_CancelTrip ON Trips INSTEAD OF DELETE
AS  
    UPDATE Trips
	   SET CancelDate = GETDATE()
	  FROM Trips AS t
	  JOIN deleted AS d
	    ON d.Id = t.Id
	 WHERE t.CancelDate IS NULL