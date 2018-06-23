SELECT c.FirstName, 
       c.LastName, 
	   c.Phone
FROM Clients AS c
ORDER BY c.LastName, c.ClientId
