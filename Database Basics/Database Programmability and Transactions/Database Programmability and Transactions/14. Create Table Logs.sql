CREATE TABLE Logs(
LogId INT PRIMARY KEY IDENTITY,
AccountId INT,
OldSum DECIMAL(15, 2),
NewSum DECIMAL(15, 2)
)
GO

CREATE TRIGGER tr_SumUpdate ON Accounts FOR UPDATE
AS
INSERT INTO Logs
SELECT a.AccountHolderId, 
       d.Balance,
       a.Balance 
FROM inserted AS a
JOIN deleted AS d
ON d.AccountHolderId = a.AccountHolderId


