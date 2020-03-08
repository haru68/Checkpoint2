USE WCS_School
GO

-------------------
-- Procedures
-------------------

--------------------------------------------------------------------
-- Afficher tous les événements auxquels une personne doit assister
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetAllEventFromUser
GO

CREATE PROCEDURE sp_GetAllEventFromUser
	@PersonId INT
	AS
	SELECT Calendar."Name", Agenda.id, Agenda."Description", Agenda.StartTime, Agenda.EndTime FROM Agenda
	INNER JOIN Calendar ON Agenda.FK_Calendar = Calendar.id
	INNER JOIN Person ON Calendar.id = Person.FK_Calendar
	WHERE Person.id = @PersonId
	RETURN
	GO

EXECUTE sp_GetAllEventFromUser 1
GO


--------------------------------------------------------------------
-- Afficher tous les événements auxquels une personne doit assister
-- en fonction de dates
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetAllEventFromUserByDate
GO

CREATE PROCEDURE sp_GetAllEventFromUserByDate
	@PersonId INT,
	@StartDate DATETIME,
	@EndDate DATETIME
	AS
	SELECT Calendar."Name", Agenda.id, Agenda."Description", Agenda.StartTime, Agenda.EndTime FROM Agenda
	INNER JOIN Calendar ON Agenda.FK_Calendar = Calendar.id
	INNER JOIN Person ON Calendar.id = Person.FK_Calendar
	WHERE Person.id = @PersonId AND Agenda.StartTime = @StartDate AND Agenda.EndTime = @EndDate
	RETURN
	GO

SELECT * FROM Agenda
GO

EXECUTE sp_GetAllEventFromUserByDate 3, '2020-29-05', '2020-30-05'
GO

--------------------------------------------------------------------
-- Afficher tous les étudiants d'un cursus
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetAllStudentsInACursus
GO

CREATE PROCEDURE sp_GetAllStudentsInACursus
	@CursusName VARCHAR(50)
	AS
	SELECT Person.id, Person.FirstName, Person.LastName, Person.Birthday, 
			Person.Email
			FROM Person
			INNER JOIN Cursus ON Cursus.id = Person.FK_Cursus
			WHERE FK_Cursus IS NOT NULL AND Cursus."Name" = @CursusName AND FK_Trainer IS NOT NULL
	RETURN
	GO

--------------------------------------------------------------------
-- Récupérer un cursus à partir de son nom
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetCursus
GO

CREATE PROCEDURE sp_GetCursus
	@CursusName VARCHAR(80)
	AS
	SELECT StartDate, EndDate FROM Cursus WHERE Cursus."Name" = @CursusName
	GO

--------------------------------------------------------------------
-- Récupérer des quêtes à partir cursus
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetQuestForACursus
GO

CREATE PROCEDURE sp_GetQuestForACursus
	@CursusName VARCHAR(80)
	AS
	SELECT Quests.id, Quests."Name", Quests."Text"
	FROM Quests
	INNER JOIN Expeditions ON Expeditions.id = Quests.FK_Expedition
	INNER JOIN Cursus ON Cursus.id = Expeditions.FK_Cursus
	WHERE Cursus.Name = @CursusName
	RETURN
	GO

--------------------------------------------------------------------
-- Récupérer des expeditions à partir cursus
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetExpeditionsForACursus
GO

CREATE PROCEDURE sp_GetExpeditionsForACursus
	@CursusName VARCHAR(80)
	AS
	SELECT Expeditions.id, Expeditions."Name" FROM Expeditions
	INNER JOIN Cursus ON Cursus.id = Expeditions.FK_Cursus
	WHERE Cursus.Name = @CursusName
	RETURN
	GO

--------------------------------------------------------------------
-- Récupérer agenda à partir cursus
--------------------------------------------------------------------


DROP PROCEDURE IF EXISTS sp_GetAgendaForACursusFromCursusName
GO

CREATE PROCEDURE sp_GetAgendaForACursusFromCursusName
	@CursusName VARCHAR(80)
	AS
	SELECT Agenda."Description", Agenda.StartTime, Agenda.EndTime FROM Agenda
	INNER JOIN Calendar ON Calendar.id = Agenda.FK_Calendar
	INNER JOIN Person ON Person.FK_Calendar = Calendar.id
	INNER JOIN Cursus ON Cursus.id = Person.FK_Cursus
	WHERE Cursus."Name" = @CursusName
	RETURN
	GO

--------------------------------------------------------------------
-- Récupérer calendrier à partir cursus
--------------------------------------------------------------------


DROP PROCEDURE IF EXISTS sp_GetCalendarForACursus
GO

CREATE PROCEDURE sp_GetCalendarForACursus
	@CursusName VARCHAR(80)
	AS
	SELECT Calendar."Name",  Calendar."Description" FROM Calendar
	INNER JOIN Cursus ON Cursus.FK_Calendar = Calendar.id
	WHERE Cursus."Name" = @CursusName
	RETURN 
	GO

	SELECT * FROM Calendar
	SELECT * FROM Cursus

	SELECT * FROM Cursus


--------------------------------------------------------------------
-- Récupérer l'adresse d'une personne à partir de son ID
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetPersonAdressFromId
GO

CREATE PROCEDURE sp_GetPersonAdressFromId
	@PersonId INT
	AS
	SELECT Adress.id, StreetNumber, StreetName, CityName, Country FROM Adress
	INNER JOIN Person ON Person.FK_Adress = Adress.id
	WHERE Person.id = @PersonId
	RETURN
	GO

--------------------------------------------------------------------
-- Récupérer l'agenda d'une personne à partir de son ID
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetPersonAgendaFromPersonId
GO

CREATE PROCEDURE sp_GetPersonAgendaFromPersonId
	@PersonId INT
	AS
	SELECT Agenda.id, Agenda.Description, Agenda.StartTime, Agenda.EndTime FROM Agenda
	INNER JOIN Calendar ON Calendar.id = Agenda.FK_Calendar
	INNER JOIN Person ON Person.FK_Calendar = Calendar.id
	WHERE Person.id = @PersonId
	RETURN
	GO

--------------------------------------------------------------------
-- Récupérer des quêtes à partir de l'id d'une expédition
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetQuestsFromExpeditionId
GO

CREATE PROCEDURE sp_GetQuestsFromExpeditionId
	@ExpeditionId INT
	AS
	SELECT Quests.id, Quests.Name, Quests.Text FROM Quests
	INNER JOIN Expeditions ON Expeditions.id = Quests.FK_Expedition
	WHERE Expeditions.id = @ExpeditionId
	RETURN 
	GO

SELECT * FROM Person
SELECT * FROM Cursus WHERE Cursus.Name = 'Cursus 1'