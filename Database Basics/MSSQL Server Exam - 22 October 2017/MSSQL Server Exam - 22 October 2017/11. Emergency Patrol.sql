SELECT r.OpenDate, 
       r.Description,
	   u.Email
FROM Reports AS r
JOIN Categories AS c
ON c.Id = r.CategoryId
JOIN Users AS u
ON u.Id = r.UserId
WHERE r.CloseDate IS NULL 
     AND LEN(r.Description) > 20 
	 AND r.Description LIKE '%STR%'
	 AND c.DepartmentId IN(1, 4, 5)
ORDER BY r.OpenDate, u.Email, r.Id