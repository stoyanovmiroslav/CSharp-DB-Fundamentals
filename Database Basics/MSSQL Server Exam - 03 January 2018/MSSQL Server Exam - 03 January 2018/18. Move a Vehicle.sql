CREATE OR ALTER PROC usp_MoveVehicle @vehicleId INT, @officeId INT
AS
DECLARE @parkinPlaces INT=
(
    SELECT o.parkingPlaces
    FROM Offices AS o
    WHERE o.Id = @officeId
);

DECLARE @parkedCars INT=
(
    SELECT COUNT(*)
    FROM Vehicles AS v
    WHERE v.OfficeId = @officeId
    GROUP BY OfficeId
);

    IF(@parkinPlaces > @parkedCars)	
        BEGIN
        	UPDATE Vehicles
        	SET OfficeId = @officeId
        	WHERE Id = @vehicleId
        END
    ELSE 
        THROW 99001, 'Not enough room in this office!', 1;
 GO

EXEC usp_MoveVehicle 7, 32;
SELECT OfficeId FROM Vehicles WHERE Id = 7

