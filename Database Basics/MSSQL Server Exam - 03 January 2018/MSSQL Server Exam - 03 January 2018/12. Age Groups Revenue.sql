WITH CTE_AgeGroupsBillMileage(AgeGroup, Bill, Mileage ) AS 
(
SELECT CASE
         WHEN YEAR(c.BirthDate) BETWEEN 1970 AND 1979 THEN '70''s'
	     WHEN YEAR(c.BirthDate) BETWEEN 1980 AND 1989 THEN '80''s'
	     WHEN YEAR(c.BirthDate) BETWEEN 1990 AND 1999 THEN '90''s'
		 ELSE 'Others'
       END AS AgeGroup, 
	   o.Bill,
	   o.TotalMileage
FROM Orders AS o
JOIN Clients AS c
ON c.Id = o.ClientId)


SELECT ag.AgeGroup,
       SUM(ag.Bill) AS Revenue,
	   AVG(ag.Mileage) AS AverageMileage
FROM CTE_AgeGroupsBillMileage AS ag
GROUP BY ag.AgeGroup
ORDER BY ag.AgeGroup ASC