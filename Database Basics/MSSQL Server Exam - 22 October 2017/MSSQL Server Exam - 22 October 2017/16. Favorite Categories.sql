WITH CTE_Departmets_Category([DepartmentName], [CategoryName], [Count]) AS(
SELECT d.Name AS [DepartmentName], 
       c.Name AS [CategoryName],
	   COUNT(*) AS [Count]
FROM Reports AS r
JOIN Categories AS c
ON c.Id = r.CategoryId
JOIN Departments AS d
ON d.Id = c.DepartmentId
GROUP BY d.Name, c.Name
),

CTE_Departmets_Total([DepartmentName], [Count]) AS(
SELECT d.Name AS [DepartmentName],
	   COUNT(*) AS [Count]
FROM Reports AS r
JOIN Categories AS c
ON c.Id = r.CategoryId
JOIN Departments AS d
ON d.Id = c.DepartmentId
GROUP BY d.Name)

SELECT dt.DepartmentName AS [Department Name], 
       dc.CategoryName AS [Category Name],
	   CAST(ROUND(CAST(dc.Count AS DECIMAL) / CAST(dt.Count AS DECIMAL) * 100, 0) AS INT)AS [Percentage]
FROM CTE_Departmets_Category AS dc
JOIN CTE_Departmets_Total AS dt
ON dt.DepartmentName = dc.DepartmentName
ORDER BY dt.DepartmentName, dc.CategoryName, [Percentage]