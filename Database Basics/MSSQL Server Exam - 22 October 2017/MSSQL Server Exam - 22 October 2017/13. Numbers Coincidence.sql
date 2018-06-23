SELECT DISTINCT u.Username
FROM users as u
JOIN Reports AS r
ON r.UserId = u.Id
WHERE LEFT(u.Username, 1) LIKE '[0-9]' AND CONVERT(NVARCHAR(10), r.CategoryId) = LEFT(u.Username, 1) OR
     RIGHT(u.Username, 1) LIKE '[0-9]' AND CONVERT(NVARCHAR(10), r.CategoryId) = RIGHT(u.Username, 1)
ORDER BY u.Username
