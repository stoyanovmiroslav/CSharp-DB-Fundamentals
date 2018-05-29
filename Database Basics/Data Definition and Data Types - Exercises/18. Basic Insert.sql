INSERT INTO Towns VALUES
('Sofia'),
('Plovdiv'),
('Varna'),
('Burgas')

INSERT INTO Addresses VALUES
('Mladost', 1),
('Sdudets City', 1),
('Center', 2),
('Dragalevci', 3),
('Dragalevci', 4)

INSERT INTO Departments VALUES
('Engineering'),
('Sales'),
('Marketing'),
('Software Development'),
('Quality Assurance')

INSERT INTO Employees VALUES
('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, CONVERT(datetime2, '01/02/2013', 103), 3500.00, 2),
('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, CONVERT(datetime2, '02/03/2004', 103), 4000.00, 1),
('Maria', 'Petrova', 'Ivanova', 'Intern', 5, CONVERT(datetime2, '28/08/2016', 103), 525.25, 3),
('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, CONVERT(datetime2, '09/12/2007', 103), 3000.00, 4),
('Peter', 'Pan', 'Pan', 'Intern', 3, CONVERT(datetime2, '28/08/2016', 103), 599.88, 2)