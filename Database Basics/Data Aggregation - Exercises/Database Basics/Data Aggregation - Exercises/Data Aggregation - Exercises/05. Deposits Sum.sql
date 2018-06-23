SELECT DepositGroup,
       SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
GROUP BY DepositGroup;


SELECT * FROM WizzardDeposits