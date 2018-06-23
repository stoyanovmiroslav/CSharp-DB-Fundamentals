SELECT c.FirstName + ' ' + c.LastName AS [Client Full Name],
       DATEDIFF(DAY, j.IssueDate, '24 April 2017') AS [Days going],
       j.Status
FROM Jobs AS j
JOIN Clients AS c
ON c.ClientId = j.ClientId
WHERE j.Status != 'Finished'
ORDER BY [Days going] DESC, c.ClientId