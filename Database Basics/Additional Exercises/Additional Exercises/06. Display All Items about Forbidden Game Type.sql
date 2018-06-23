SELECT i.Name AS Item, 
       i.Price AS Price,
	   i.MinLevel AS MinLevel, 
	   gt.Name AS [Forbidden Game Type]
FROM GameTypes as gt
INNER JOIN GameTypeForbiddenItems AS gtfi
ON gtfi.GameTypeId = gt.Id
RIGHT JOIN Items AS i
ON i.Id = gtfi.ItemId
ORDER BY gt.Name DESC, i.Name