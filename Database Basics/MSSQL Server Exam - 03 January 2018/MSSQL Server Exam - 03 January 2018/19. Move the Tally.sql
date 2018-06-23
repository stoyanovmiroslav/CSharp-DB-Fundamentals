CREATE OR ALTER TRIGGER tr_TotalMileage ON Orders AFTER UPDATE
AS
DECLARE @DeletedMileage INT = (SELECT d.TotalMileage FROM deleted AS d)
DECLARE @InsertedMileage INT = (SELECT i.TotalMileage FROM inserted AS i)
DECLARE @VehicleId INT = (SELECT i.VehicleId FROM inserted AS i)
  
  UPDATE Vehicles
  SET Mileage += @InsertedMileage
  WHERE Id = @VehicleId

IF(@DeletedMileage IS NOT NULL)
BEGIN
   ROLLBACK
   RETURN
END