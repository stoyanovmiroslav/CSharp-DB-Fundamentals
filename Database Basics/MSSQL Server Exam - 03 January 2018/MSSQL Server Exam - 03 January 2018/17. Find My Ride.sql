CREATE OR ALTER FUNCTION udf_CheckForVehicle(@townName NVARCHAR(50), @seatsNumber INT)
RETURNS NVARCHAR(MAX)
AS BEGIN
    DECLARE @OfficeNameAndModel NVARCHAR(MAX) = (
			    SELECT TOP(1) o.Name + ' - ' +  m.Model
				FROM Vehicles AS v
				 JOIN Models AS m
				  ON m.Id = v.ModelId
				 JOIN Offices AS o
				  ON o.Id = v.OfficeId
				 JOIN Towns AS t
				  ON t.Id = o.TownId
				WHERE t.Name = @townName AND m.Seats = @seatsNumber
				ORDER BY o.Name)
       
	   IF(@OfficeNameAndModel IS NULL)
	   BEGIN 
	   RETURN 'NO SUCH VEHICLE FOUND'
	   END

	   RETURN @OfficeNameAndModel
 END
 GO

SELECT dbo.udf_CheckForVehicle('La Escondida', 9)
