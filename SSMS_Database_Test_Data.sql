--------------------------------------------------
------------------  Menu -------------------------

-- 1) Create DB
-- 2) Create tables
-- 3) Insert into Cursus
-- 4) Insert into expedition
-- 5) Insert into  quest
-- 6) Insert into  adress
-- 7) Insert into agenda
-- 8) Insert into Person

----------------------------------------------------


USE [master];

DECLARE @kill varchar(8000) = '';  
SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
FROM sys.dm_exec_sessions
WHERE database_id  = db_id('WCS_School')

EXEC(@kill);

DROP DATABASE IF EXISTS WCS_School
GO

CREATE DATABASE WCS_School
GO

USE WCS_School
GO

-------------------
-- Tables creation
-------------------
CREATE TABLE Calendar (
id INT PRIMARY KEY IDENTITY(1,1),
"Name" VARCHAR (50),
"Description" TEXT
)
GO

CREATE TABLE Cursus (
id INT PRIMARY KEY IDENTITY(1,1),
"Name" VARCHAR (80) NOT NULL,
StartDate DATE NOT NULL,
EndDate DATE NOT NULL,
FK_Calendar INT NOT NULL,
FOREIGN KEY (FK_Calendar) REFERENCES Calendar(id)
)
GO


CREATE TABLE Expeditions (
id INT PRIMARY KEY IDENTITY(1,1),
"Name" VARCHAR (50) NOT NULL,
FK_Cursus INT NOT NULL,
FOREIGN KEY (FK_Cursus) REFERENCES Cursus(id)
)
GO

CREATE TABLE Quests (
id INT PRIMARY KEY IDENTITY(1,1),
"Name" VARCHAR(50) NOT NULL,
"Text" Text NOT NULL,
FK_Expedition INT,
FOREIGN KEY (FK_Expedition) REFERENCES Expeditions(id)
)
GO


CREATE TABLE Adress(
id INT PRIMARY KEY IDENTITY(1,1),
StreetNumber INT NOT NULL,
StreetName VARCHAR(150) NOT NULL,
CityName VARCHAR(100) NOT NULL,
Country VARCHAR(80) NOT NULL
)
GO


CREATE TABLE Agenda(
id INT PRIMARY KEY IDENTITY(1,1),
FK_Calendar INT,
FOREIGN KEY (FK_Calendar) REFERENCES Calendar(id),
"Description" VARCHAR (250) NOT NULL,
StartTime DATETIME NOT NULL,
EndTime DATETIME NOT NULL
)
GO

CREATE TABLE Person (
id INT PRIMARY KEY IDENTITY(1,1),
FirstName VARCHAR(50) NOT NULL,
LastName VARCHAR(80) NOT NULL,
Birthday DATETIME,
FK_Adress INT NOT NULL,
FOREIGN KEY (FK_Adress) REFERENCES Adress(id),
Email VARCHAR(80) NOT NULL,
FK_Cursus INT,
FOREIGN KEY (FK_Cursus) REFERENCES Cursus(id),
FK_Calendar INT,
FOREIGN KEY (FK_Calendar) REFERENCES Calendar(id),
FK_Trainer INT,
FOREIGN KEY (FK_Trainer) REFERENCES Person(id),
FK_LeadingTrainer INT,
FOREIGN KEY (FK_LeadingTrainer) REFERENCES Person(id)
)
GO


-------------------
-- Calendar creation
-------------------

DECLARE @CounterCalendar INT = 0
WHILE (@CounterCalendar < 5)
BEGIN
	INSERT INTO Calendar ("Name", "Description")
	VALUES
	(CONCAT('Calendrier ',@CounterCalendar),'Je suis un calendrier bien rempli')
	SET @CounterCalendar = @CounterCalendar + 1
END
GO

-------------------
-- Agenda creation
-------------------

DECLARE @CounterCalendar INT = 1
DECLARE @CounterAgenda INT = 0
WHILE(@CounterCalendar <= 5)
BEGIN
	WHILE (@CounterAgenda < 10)
	BEGIN
		INSERT INTO Agenda (FK_Calendar, "Description", StartTime, EndTime)
		VALUES
		(@CounterCalendar, 'Je suis un évenement dans un agenda', '2020-29-05', '2020-30-05')
		SET @CounterAgenda = @CounterAgenda + 1
	END
	SET @CounterAgenda = 0
	SET @CounterCalendar = @CounterCalendar + 1
END
GO

------------------
-- Cursus creation
------------------


DECLARE @CounterCursus INT = 1
WHILE (@CounterCursus <= 5)
BEGIN
	INSERT INTO Cursus ("Name", StartDate, EndDate, FK_Calendar)
	VALUES
	(CONCAT('Cursus ', @CounterCursus), '2019-12-08', '2020-08-08', @CounterCursus)
	SET @CounterCursus = @CounterCursus + 1
END
GO


----------------------
-- Expedition creation
----------------------

DECLARE @CounterCursus INT = 1
DECLARE @CounterExpeditions INT = 0
WHILE (@CounterCursus <= 5)
BEGIN
		WHILE (@CounterExpeditions < 10)
		BEGIN
		INSERT INTO Expeditions ("name", FK_Cursus)
		VALUES
		(CONCAT('Expedition ', @CounterExpeditions), @CounterCursus)
		SET @CounterExpeditions = @CounterExpeditions + 1
	END
	SET @CounterCursus = @CounterCursus + 1
	SET @CounterExpeditions = 0
END
GO

------------------
-- Quest creation
------------------

DECLARE @CounterQuest INT = 0
DECLARE @CounterExpedition INT = 1
WHILE (@CounterQuest < 200)
BEGIN
	WHILE(@CounterExpedition <= 50)
	BEGIN
		INSERT INTO Quests ("Name", "Text", FK_Expedition)
		VALUES
		(CONCAT('Quest', @CounterQuest), 'Ceci est une superbe quête rédigée par un très bon formateur', @CounterExpedition)
		SET @CounterExpedition = @CounterExpedition +1
		SET @CounterQuest = @CounterQuest + 1
	END
	SET @CounterExpedition = 1
END
GO

-------------------
-- Adress creation
-------------------

DECLARE @CounterPerson INT = 0
WHILE (@CounterPerson < 95)
BEGIN
	INSERT INTO Adress (StreetNumber, StreetName, CityName, Country)
	VALUES
	(@CounterPerson, 'Street', CONCAT('City ', @CounterPerson), 'France')
	SET @CounterPerson = @CounterPerson + 1
END



--------------------
-- Person Creation
--------------------

DECLARE @CounterLeadingTrainer INT = 0
DECLARE @CounterCalendar INT = 1
WHILE (@CounterLeadingTrainer < 5)
BEGIN
	INSERT INTO Person ("FirstName", LastName, Birthday, FK_Adress, Email, FK_Calendar)
	VALUES
	('Leading Formateur', 'NOM', '1800-01-1', (@CounterLeadingTrainer+1), 'leadingTrainer@mail', @CounterCalendar)
	SET @CounterLeadingTrainer = @CounterLeadingTrainer + 1
	SET @CounterCalendar = @CounterCalendar + 1
	IF (@CounterCalendar > 2)
	BEGIN
		SET @CounterCalendar = 1
	END
END
GO


DECLARE @CounterTrainer INT = 0
DECLARE @CursorLeadingTrainer INT
DECLARE @CounterAdress INT = 6
DECLARE @TotalAdress INT = (SELECT COUNT(id) FROM Adress)
DECLARE @CounterCursus INT =1
DECLARE @TotalCursus INT = (SELECT COUNT(id) FROM Cursus)
DECLARE @CounterCalendar INT = 1

DECLARE Cursor_LeadingTrainer CURSOR SCROLL FOR
	SELECT id FROM Person WHERE (FK_Trainer IS NULL) AND (FK_LeadingTrainer IS NULL)
OPEN Cursor_LeadingTrainer
FETCH FIRST FROM Cursor_LeadingTrainer INTO @CursorLeadingTrainer
WHILE @@FETCH_STATUS = 0
BEGIN
	WHILE (@CounterTrainer < 4)
	BEGIN
		INSERT INTO Person (FirstName, LastName, Birthday, FK_Adress, Email, FK_Cursus, FK_Calendar, FK_LeadingTrainer)
		VALUES
		('Trainer', 'NOM', '1915-05-01', @CounterAdress, 'formateur@mail', @CounterCursus, @CounterCalendar, @CursorLeadingTrainer)

		SET @CounterAdress = @CounterAdress + 1
		IF (@CounterAdress > @TotalAdress)
		BEGIN
			SET @CounterAdress = 1
		END

		SET @CounterCursus = @CounterCursus + 1
		IF (@CounterCursus > @TotalCursus)
		BEGIN
			SET @CounterCursus = 1
		END

		SET @CounterCalendar = @CounterCalendar + 1
		IF (@CounterCalendar > 4)
		BEGIN
			SET @CounterCalendar = 1
		END

		SET @CounterTrainer = @CounterTrainer + 1
	END
	SET @CounterTrainer = 0
	FETCH NEXT FROM Cursor_LeadingTrainer INTO @CursorLeadingTrainer
END
CLOSE Cursor_LeadingTrainer
DEALLOCATE Cursor_LeadingTrainer
GO
	

DECLARE @CounterStudent INT = 0
DECLARE @CursorTrainer INT
DECLARE @CounterAdress INT = 1
DECLARE @TotalAdress INT = (SELECT COUNT(id) FROM Adress)
DECLARE @CounterCursus INT =1
DECLARE @TotalCursus INT = (SELECT COUNT(id) FROM Cursus)
DECLARE @CounterCalendar INT = 4

DECLARE Cursor_Trainer CURSOR SCROLL FOR
	SELECT id FROM Person WHERE (FK_LeadingTrainer IS NOT NULL)
OPEN Cursor_Trainer
FETCH FIRST FROM Cursor_Trainer INTO @CursorTrainer

WHILE @@FETCH_STATUS = 0
BEGIN
	WHILE(@CounterStudent < 10)
	BEGIN
		--PRINT @CursorTrainer
		INSERT INTO Person (FirstName, LastName, Birthday, FK_Adress, Email, FK_Cursus, FK_Calendar, FK_Trainer)
		VALUES
		('Student', 'NOM', '1980-05-05', @CounterAdress, 'student@mail', @CounterCursus, @CounterCalendar, @CursorTrainer)
		SET @CounterStudent = @CounterStudent + 1

		SET @CounterAdress = @CounterAdress + 1
		IF (@CounterAdress > @TotalAdress)
		BEGIN
			SET @CounterAdress = 1
		END

		SET @CounterCursus = @CounterCursus + 1
		IF (@CounterCursus > @TotalCursus)
		BEGIN
			SET @CounterCursus = 1
		END

		SET @CounterCalendar = @CounterCalendar + 1
		IF (@CounterCalendar > 5)
		BEGIN
			SET @CounterCalendar = 4
		END
	END
	FETCH NEXT FROM Cursor_Trainer INTO @CursorTrainer
	SET @CounterStudent = 0
END
CLOSE Cursor_Trainer
DEALLOCATE Cursor_Trainer
GO




/*
SELECT * FROM Adress
SELECT * FROM Agenda
SELECT * FROM Calendar
SELECT * FROM  Cursus
SELECT * FROM  Expeditions
SELECT * FROM  Quests
SELECT * FROM Person
*/