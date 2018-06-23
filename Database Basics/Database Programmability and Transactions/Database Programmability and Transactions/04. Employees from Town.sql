CREATE PROC usp_GetEmployeesFromTown @Town NVARCHAR(MAX)
AS
     BEGIN
         SELECT e.FirstName,
                e.LastName
         FROM Addresses AS a
              INNER JOIN Towns AS t ON t.TownID = a.TownID
              INNER JOIN Employees AS e ON e.AddressID = a.AddressID
         WHERE t.Name = @Town;
     END;
