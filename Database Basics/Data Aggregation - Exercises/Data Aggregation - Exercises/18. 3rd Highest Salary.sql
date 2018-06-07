SELECT DepartmentID,
       Salary 
FROM
(
    SELECT DepartmentID,
           Salary,
           DENSE_RANK() OVER(PARTITION BY DepartmentID ORDER BY Salary DESC) AS RowNumber
    FROM Employees
) AS ThirdHighestSalary
WHERE RowNumber = 3
GROUP BY DepartmentID, Salary


SELECT e.DepartmentID,
(
    SELECT DISTINCT Salary
    FROM Employees
    WHERE DepartmentID = e.DepartmentID
    ORDER BY Salary DESC
    OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY
) AS ThirdHighestSalary
FROM Employees AS e
WHERE (
    SELECT DISTINCT Salary
    FROM Employees
    WHERE DepartmentID = e.DepartmentID
    ORDER BY Salary DESC
    OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY
) IS NOT NULL
GROUP BY e.DepartmentID