WITH EmailProvider_CTE ([Email Provider])  
AS(  SELECT SUBSTRING(u.Email, CHARINDEX('@', u.Email) + 1, LEN(u.Email)) 
AS [Email Provider]
FROM Users AS u)

SELECT e.[Email Provider],
       COUNT(e.[Email Provider]) AS [Number Of Users]
FROM EmailProvider_CTE AS e
GROUP BY e.[Email Provider]
HAVING  COUNT(e.[Email Provider]) > 0
ORDER BY  [Number Of Users] DESC, e.[Email Provider]
