SELECT Username, IpAddress
FROM Users
WHERE IpAddress LIKE '___.1[0-9]%.[0-9]%.___'
ORDER BY Username ASC

INSERT INTO Users(UserName, RegistrationDate, IsDeleted, IpAddress) VALUES
('dasdasd', '2014-01-01', 0, '192.157..222')


SELECT * FROM Users
