SELECT MIN(AverageSalary) AS MinAverageSalary
FROM (
SELECT
       AVG(e.Salary) AS AverageSalary
FROM Employees AS e
GROUP BY e.DepartmentID) AS DepartmentsAverageSalaries


SELECT TOP(1)
       AVG(e.Salary) AS MinAverageSalary
FROM Employees AS e
GROUP BY e.DepartmentID
ORDER BY AVG(e.Salary)