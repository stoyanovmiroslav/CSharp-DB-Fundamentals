CREATE TABLE NotificationEmails(
Id INT PRIMARY KEY IDENTITY,
Recipient NVARCHAR(MAX),
[Subject] NVARCHAR(MAX),
Body NVARCHAR(MAX))
GO

CREATE OR ALTER TRIGGER tr_LogsUpdate ON Logs FOR INSERT
AS
INSERT INTO NotificationEmails
SELECT i.AccountId,
      CONCAT('Balance change for account: ', i.AccountId), 
	  CONCAT('On ', GETDATE(), ' your balance was changed from ', i.OldSum, ' to ', i.NewSum, '.')
FROM inserted as i



select *
from 
NotificationEmails

select * from Logs

update Accounts
set Balance += 100


