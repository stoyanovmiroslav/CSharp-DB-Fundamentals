CREATE TRIGGER tr_ItemsPurchaseRestrictions ON UserGameItems AFTER INSERT
AS
BEGIN
  DECLARE @userGameLevel int = (
    SELECT ug.Level
    FROM inserted AS ugi -- UserGameItems
    JOIN UsersGames AS ug ON ugi.UserGameId = ug.Id
  );
  DECLARE @itemMinLevel int = (
    SELECT i.MinLevel
    FROM inserted AS ugi -- UserGameItems
    JOIN Items AS i on i.Id = ugi.ItemId
  );
  IF(@itemMinLevel > @userGameLevel)
    BEGIN
      ROLLBACK;
      RAISERROR('Higher user game level required for item purchase', 16, 1);
      RETURN;
    END
END

INSERT INTO UserGameItems (ItemId, UserGameId) VALUES (3, 2);
INSERT INTO UserGameItems (ItemId, UserGameId) VALUES (4, 3);