CREATE TABLE Directors(
Id INT PRIMARY KEY IDENTITY,
DirectorName NVARCHAR(30) NOT NULL,
Notes NVARCHAR(90) 
)

CREATE TABLE Genres(
Id INT PRIMARY KEY IDENTITY,
GenreName NVARCHAR(30) NOT NULL,
Notes NVARCHAR(90)
)

CREATE TABLE Categories(
Id INT PRIMARY KEY IDENTITY,
CategoryName NVARCHAR(30) NOT NULL,
Notes NVARCHAR(90)
)

CREATE TABLE Movies(
Id INT PRIMARY KEY IDENTITY,
Title NVARCHAR(30) NOT NULL,
DirectorId INT FOREIGN KEY (DirectorId) REFERENCES Directors(Id) NOT NULL,
CopyrightYear DATETIME2 NOT NULL, 
[Length] NVARCHAR(30) NOT NULL,
GenreId INT FOREIGN KEY (GenreId) REFERENCES Genres(Id) NOT NULL,
CategoryId INT FOREIGN KEY (CategoryId) REFERENCES Categories(Id) NOT NULL,
Rating INT NOT NULL,
Notes NVARCHAR(90)
)

INSERT INTO Directors VALUES
('Steven Spielberg', 'Steven Spielberg is born..'),
('Stephen King1', 'Stephen King is born1..'),
('Stephen King2', 'Stephen King is born2..'),
('Stephen King3', 'Stephen King is born.3.'),
('Stephen King4', 'Stephen King is born4..')

INSERT INTO Genres VALUES
('Comedy', 'Comedy is..'),
('Actions1', 'Actions is1..'),
('Actions2', 'Actions is3..'),
('Actions3', 'Actions is4..'),
('Actions4', 'Actions is2..')

INSERT INTO Categories VALUES
('13+', 'The Movie should12...'),
('18+', 'The Movie should23...'),
('Animation1', 'The Movie should1...'),
('Animation3', 'The Movie should3...'),
('Animation', 'The Movie should...')


INSERT INTO Movies VALUES
('Transporter', 1, '2017-01-01', '1:46', 2, 1, 5, 'The Best6..'),
('Transporter2', 2, '2018-01-01', '1:38', 2, 1, 6, 'The Best Movie for 2018'),
('Chas pik', 1, '2017-01-01', '1:36', 1, 2, 5, 'The Best4..'),
('Transporter1', 2, '2017-01-01', '1:46', 2, 1, 5, 'The Best2..'),
('Transporter2', 2, '2017-01-01', '1:46', 2, 1, 5, 'The Best1..')