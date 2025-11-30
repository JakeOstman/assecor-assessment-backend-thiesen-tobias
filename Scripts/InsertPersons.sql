SET IDENTITY_INSERT Person ON;

SET IDENTITY_INSERT Person ON;

INSERT INTO Person (Id, Name, LastName, ZipCode, City, Color) VALUES (0, 'Hans', 'Müller', '67742', 'Lauterecken', 'blau');
INSERT INTO Person (Id, Name, LastName, ZipCode, City, Color) VALUES (1, 'Peter', 'Petersen', '18439', 'Stralsund', 'grün');
INSERT INTO Person (Id, Name, LastName, ZipCode, City, Color) VALUES (2, 'Johnny', 'Johnson', '88888', 'made up', 'violett');
INSERT INTO Person (Id, Name, LastName, ZipCode, City, Color) VALUES (3, 'Milly', 'Millenium', '77777', 'made up too', 'rot');
INSERT INTO Person (Id, Name, LastName, ZipCode, City, Color) VALUES (4, 'Jonas', 'Müller', '32323', 'Hansstadt', 'gelb');
INSERT INTO Person (Id, Name, LastName, ZipCode, City, Color) VALUES (5, 'Tastatur', 'Fujitsu', '42342', 'Japan', 'türkis');
INSERT INTO Person (Id, Name, LastName, ZipCode, City, Color) VALUES (6, 'Anders', 'Andersson', '32132', 'Schweden', 'grün');
INSERT INTO Person (Id, Name, LastName, ZipCode, City, Color) VALUES (7, 'Bertram', 'Bart', '12313', 'Wasweißich', 'blau');
INSERT INTO Person (Id, Name, LastName, ZipCode, City, Color) VALUES (8, 'Gerda', 'Gerber', '76535', 'Woanders', 'violett');
INSERT INTO Person (Id, Name, LastName, ZipCode, City, Color) VALUES (9, 'Klaus', 'Klaussen', '43246', 'Hierach', 'grün');

SET IDENTITY_INSERT Person OFF;


SET IDENTITY_INSERT Person OFF;

DECLARE @MaxId INT;
SELECT @MaxId = MAX(Id) FROM Person;
DBCC CHECKIDENT ('Person', RESEED, @MaxId);