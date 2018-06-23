SELECT m.FirstName + ' ' + m.LastName AS [Full Name],
       j.Status,
	   j.IssueDate
FROM Jobs AS j
JOIN Mechanics AS m
ON m.MechanicId = j.MechanicId 
ORDER BY m.MechanicId, j.IssueDate, j.JobId

--Select all mechanics with their jobs. Include job status and issue date. Order by mechanic Id, issue date, job Id (all ascending).
--Required columns:
--•	Mechanic Full Name
--•	Job Status
--•	Job Issue Date
