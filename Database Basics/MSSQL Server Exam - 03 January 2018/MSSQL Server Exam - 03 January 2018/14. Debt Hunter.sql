WITH CTE_ClientsWithInvalidCards(ClientId ,ClientName, Email, Bill, TownName, Rank)
AS(
SELECT c.Id,
       c.FirstName + ' ' + c.LastName AS Name,
       c.Email,
	   o.Bill,
	   t.Name, 
	  DENSE_RANK() OVER   
      (PARTITION BY t.Name ORDER BY o.Bill DESC) AS Rank
from orders AS o
JOIN Clients AS c
ON c.Id = o.ClientId
JOIN Towns as t
ON t.Id = o.TownId
WHERE c.CardValidity < o.CollectionDate)

SELECT cic.ClientName AS [Category Name],
       cic.Email,
	   cic.Bill,
	   cic.TownName AS Town
FROM CTE_ClientsWithInvalidCards cic
WHERE cic.Rank IN(1, 2) AND cic.Bill IS NOT NULL
ORDER BY cic.TownName, cic.Bill ASC, cic.ClientId
