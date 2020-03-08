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

CREATE TABLE Adress(
id INT PRIMARY KEY IDENTITY(1,1),
StreetNumber INT NOT NULL,
StreetName VARCHAR(150) NOT NULL,
CityName VARCHAR(100) NOT NULL,
Country VARCHAR(80) NOT NULL
)
GO

CREATE TABLE Campus (
id INT PRIMARY KEY IDENTITY(1,1),
"Name" VARCHAR (50),
FK_Adress INT,
FOREIGN KEY (FK_Adress) REFERENCES Adress(id)
)
GO

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
FOREIGN KEY (FK_Calendar) REFERENCES Calendar(id),
FK_Campus INT NOT NULL,
FOREIGN KEY (FK_Campus) REFERENCES Campus(id)
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
FK_Trainer INT,
FOREIGN KEY (FK_Trainer) REFERENCES Person(id),
FK_LeadingTrainer INT,
FOREIGN KEY (FK_LeadingTrainer) REFERENCES Person(id)
)
GO

-------------------
-- Adress creation
-------------------

DECLARE @CounterAdress INT = 0
WHILE (@CounterAdress < 100)
BEGIN
	INSERT INTO Adress (StreetNumber, StreetName, CityName, Country)
	VALUES
	(@CounterAdress, 'Street', CONCAT('City ', @CounterAdress), 'France')
	SET @CounterAdress = @CounterAdress + 1
END

-------------------
-- Campus creation
-------------------

DECLARE @CounterCampus INT = 1
WHILE (@CounterCampus <= 3)
BEGIN
	INSERT INTO Campus ("Name", FK_Adress)
	VALUES
	(CONCAT('Campus', @CounterCampus), @CounterCampus)
	SET @CounterCampus = @CounterCampus + 1
END


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
DECLARE @CursorCalendar INT
DECLARE Cursor_Calendar Cursor SCROLL FOR
	SELECT id FROM Calendar
OPEN Cursor_Calendar
FETCH FIRST FROM Cursor_Calendar INTO @CursorCalendar
WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @CounterAgenda INT = 1
	WHILE (@CounterAgenda <= 10)
	BEGIN
		INSERT INTO Agenda (FK_Calendar, "Description", StartTime, EndTime)
		VALUES
		(@CursorCalendar, CONCAT('Event dans agenda', @CursorCalendar, 't', @CounterAgenda), '2020-29-05', '2020-30-05')
		SET @CounterAgenda = @CounterAgenda + 1
	END
	SET @CounterAgenda = 0
	FETCH NEXT FROM Cursor_Calendar INTO @CursorCalendar
END
CLOSE Cursor_Calendar
DEALLOCATE Cursor_Calendar
GO

------------------
-- Cursus creation
------------------

DECLARE @CursorCampus INT
DECLARE Cursor_Campus CURSOR SCROLL FOR
	SELECT id FROM Campus
OPEN Cursor_Campus
FETCH FIRST FROM Cursor_Campus INTO @CursorCampus
WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @CounterCursus INT = 1
	WHILE (@CounterCursus <= 3)
	BEGIN
		INSERT INTO Cursus ("Name", StartDate, EndDate, FK_Calendar, FK_Campus)
		VALUES
		(CONCAT('Cursus ', @CounterCursus), '2019-12-08', '2020-08-08', @CounterCursus, @CursorCampus)
		SET @CounterCursus = @CounterCursus + 1
	END
	FETCH NEXT FROM Cursor_Campus INTO @CursorCampus
END
CLOSE Cursor_Campus
DEALLOCATE Cursor_Campus
GO

----------------------
-- Expedition creation
----------------------

DECLARE @CursorCursus INT
DECLARE Cursor_Cursus CURSOR SCROLL FOR
	SELECT id FROM Cursus
OPEN Cursor_Cursus
FETCH FIRST FROM Cursor_Cursus INTO @CursorCursus
WHILE @@FETCH_STATUS = 0
BEGIN
		DECLARE @CounterExpeditions INT = 1
		WHILE (@CounterExpeditions <= 5)
		BEGIN
			INSERT INTO Expeditions ("name", FK_Cursus)
			VALUES
			(CONCAT('Expedition ', @CounterExpeditions), @CursorCursus)
			SET @CounterExpeditions = @CounterExpeditions + 1
		END
		SET @CounterExpeditions = 0
		
	FETCH NEXT FROM Cursor_Cursus INTO @CursorCursus
END
CLOSE Cursor_Cursus
DEALLOCATE Cursor_Cursus
GO

------------------
-- Quest creation
------------------

DECLARE @CounterQuest INT = 0
DECLARE @CursorExpedition INT
DECLARE Cursor_Expedition CURSOR SCROLL FOR
	SELECT id FROM Expeditions
OPEN Cursor_Expedition
FETCH FIRST FROM Cursor_Expedition INTO @CursorExpedition
WHILE @@FETCH_STATUS = 0
BEGIN
	WHILE (@CounterQuest < 30)
	BEGIN
			INSERT INTO Quests ("Name", "Text", FK_Expedition)
			VALUES
			(CONCAT('Quest', @CursorExpedition, 't', @CounterQuest), 'Ceci est une superbe quête rédigée par un très bon formateur', @CursorExpedition)
			SET @CounterQuest = @CounterQuest + 1
	END
	SET @CounterQuest = 0
	FETCH NEXT FROM Cursor_Expedition INTO @CursorExpedition
END
CLOSE Cursor_Expedition
DEALLOCATE Cursor_Expedition
GO

--------------------
-- Person Creation
--------------------

--Leading Trainer
DECLARE @CursorCursus INT
DECLARE Cursor_Cursus CURSOR SCROLL FOR
	SELECT id FROM Cursus
OPEN Cursor_Cursus
FETCH FIRST FROM Cursor_Cursus INTO @CursorCursus
WHILE @@FETCH_STATUS = 0
BEGIN
	DECLARE @CounterLeadingTrainer INT = 1
	WHILE (@CounterLeadingTrainer <= 3)
	BEGIN
		INSERT INTO Person ("FirstName", LastName, Birthday, FK_Adress, Email, FK_Cursus)
		VALUES
		('Leading Formateur', CONCAT('Number', @CursorCursus), '1800-01-1', (@CounterLeadingTrainer+1), 'leadingTrainer@mail', @CursorCursus)
		SET @CounterLeadingTrainer = @CounterLeadingTrainer + 1
	END
	SET @CounterLeadingTrainer = 0
	FETCH NEXT FROM Cursor_Cursus
END
CLOSE Cursor_Cursus
DEALLOCATE Cursor_Cursus
GO

-- Trainer
DECLARE @CounterTrainer INT = 1
DECLARE @CursorLeadingTrainer INT
DECLARE @CounterAdress INT = 6
DECLARE @TotalAdress INT = (SELECT COUNT(id) FROM Adress)
DECLARE @TotalCursus INT = (SELECT COUNT(id) FROM Cursus)

DECLARE Cursor_LeadingTrainer CURSOR SCROLL FOR
		SELECT id FROM Person WHERE (FK_Trainer IS NULL) AND (FK_LeadingTrainer IS NULL)
	OPEN Cursor_LeadingTrainer


DECLARE @FK_CursusFromLeadingTrainer INT
	
FETCH FIRST FROM Cursor_LeadingTrainer INTO @CursorLeadingTrainer
WHILE @@FETCH_STATUS = 0
BEGIN
	WHILE (@CounterTrainer <= 3)
	BEGIN
		SET @FK_CursusFromLeadingTrainer = (SELECT FK_Cursus FROM Person WHERE Person.id = @CursorLeadingTrainer)
		INSERT INTO Person (FirstName, LastName, Birthday, FK_Adress, Email, FK_Cursus, FK_LeadingTrainer)
		VALUES
		('Trainer', CONCAT('Number', @CursorLeadingTrainer), '1915-05-01', @CounterAdress, 'formateur@mail', @FK_CursusFromLeadingTrainer, @CursorLeadingTrainer)

		SET @CounterAdress = @CounterAdress + 1
		IF (@CounterAdress > @TotalAdress)
		BEGIN
			SET @CounterAdress = 1
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
DECLARE @CounterCursus INT = 1
DECLARE @TotalCursus INT = (SELECT COUNT(id) FROM Cursus)

DECLARE Cursor_Trainer CURSOR SCROLL FOR
	SELECT id FROM Person WHERE (FK_LeadingTrainer IS NOT NULL)
OPEN Cursor_Trainer
FETCH FIRST FROM Cursor_Trainer INTO @CursorTrainer

WHILE @@FETCH_STATUS = 0
BEGIN
	WHILE(@CounterStudent < 10)
	BEGIN
		SET @CounterCursus = (SELECT FK_Cursus FROM Person WHERE id = @CursorTrainer)
		INSERT INTO Person (FirstName, LastName, Birthday, FK_Adress, Email, FK_Cursus, FK_Trainer)
		VALUES
		('Student', CONCAT('Number', @CursorTrainer), '1980-05-05', @CounterAdress, 'student@mail', @CounterCursus, @CursorTrainer)
		SET @CounterStudent = @CounterStudent + 1

		SET @CounterAdress = @CounterAdress + 1
		IF (@CounterAdress > @TotalAdress)
		BEGIN
			SET @CounterAdress = 1
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