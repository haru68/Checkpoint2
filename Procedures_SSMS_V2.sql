USE WCS_School
GO

-------------------
-- Procedures
-------------------

--------------------------------------------------------------------
-- Afficher tous les événements auxquels une personne doit assister
-- en fonction de dates
--------------------------------------------------------------------
DROP PROCEDURE IF EXISTS sp_DisplayAllEventFromUserByDate
GO

CREATE PROCEDURE sp_DisplayAllEventFromUserByDate
	@PersonId INT,
	@StartDate DATETIME,
	@EndDate DATETIME
	AS
	SELECT Calendar."Name", Agenda.id, Agenda."Description", Agenda.StartTime, Agenda.EndTime FROM Agenda
	INNER JOIN Calendar ON Calendar.id = Agenda.FK_Calendar
	INNER JOIN Cursus ON Cursus.FK_Calendar = Calendar.id
	INNER JOIN Person ON Person.FK_Cursus = Cursus.id
	WHERE Person.id = 5 AND Agenda.StartTime >= @StartDate AND Agenda.EndTime <= @EndDate
	RETURN
	GO

--------------------------------------------------------------------
-- Afficher tous les étudiants d'un cursus
--------------------------------------------------------------------
DROP PROCEDURE IF EXISTS sp_DisplayAllStudentsFromACursus
GO

CREATE PROCEDURE sp_DisplayAllStudentsFromACursus
	@CursusId INT
	AS
	SELECT Person.id, Person.FirstName, Person.LastName, Person.Email, Person.Birthday,
	Adress.StreetNumber, Adress.StreetName, Adress.CityName, Adress.Country FROM Person
	INNER JOIN Cursus ON Cursus.id = Person.FK_Cursus
	INNER JOIN Adress ON Adress.id = Person.FK_Adress
	WHERE Cursus.id = @CursusId AND FK_Trainer IS NOT NULL AND FK_LeadingTrainer IS NULL
	RETURN
GO

--------------------------------------------------------------------
-- Retourne un cursus depuis son id
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetCursusFromId
GO

CREATE PROCEDURE sp_GetCursusFromId
	@CursusId INT
	AS
	SELECT Cursus.Name, Cursus.StartDate, Cursus.EndDate FROM Cursus
	WHERE Cursus.id = @CursusId
	RETURN
	GO

--------------------------------------------------------------------
-- Affiche toutes les quêtes d'un cursus
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_DisplayAllQuestsFromACursus
GO

CREATE PROCEDURE sp_DisplayAllQuestsFromACursus
	@CursusId INT
	AS
	SELECT Quests.id, Quests.Name, Quests.Text, Expeditions.Name FROM Quests
	INNER JOIN Expeditions ON Expeditions.id = Quests.FK_Expedition
	INNER JOIN Cursus ON Cursus.id = Expeditions.FK_Cursus
	WHERE Cursus.id = @CursusId
	RETURN
	GO

--------------------------------------------------------------------
-- Retourne une liste d'évenements contenu dans un cursus ID
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetAllEventsFromCursusId
GO

CREATE PROCEDURE sp_GetAllEventsFromCursusId
	@CursusId INT
	AS
	SELECT Agenda.Description, Agenda.StartTime, Agenda.EndTime FROM Agenda
	INNER JOIN Calendar ON Calendar.id = Agenda.FK_Calendar
	INNER JOIN Cursus ON Cursus.FK_Calendar = Calendar.id
	WHERE Cursus.id = @CursusId
	RETURN
	GO

--------------------------------------------------------------------
-- Retourne une liste de calendrier contenu dans un cursus ID
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetAllCalendarFromCursusId
GO

CREATE PROCEDURE sp_GetAllCalendarFromCursusId
	@CursusId INT
	AS
	SELECT Calendar.id, Calendar.Name, Calendar.Description FROM Calendar
	INNER JOIN Cursus ON Cursus.FK_Calendar = Calendar.id
	WHERE Cursus.id = @CursusId
	RETURN
	GO

--------------------------------------------------------------------
-- Récupère toutes les quêtes d'un cursus
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetAllQuestsFromACursus
GO

CREATE PROCEDURE sp_GetAllQuestsFromACursus
	@CursusId INT
	AS
	SELECT Quests."Name", Quests."Text" FROM Quests
	INNER JOIN Expeditions ON Expeditions.id = Quests.FK_Expedition
	INNER JOIN Cursus ON Cursus.id = Expeditions.FK_Cursus
	WHERE Cursus.id = @CursusId
	RETURN
	GO

--------------------------------------------------------------------
-- Récupère toutes les expéditions d'un cursus
--------------------------------------------------------------------

DROP PROCEDURE IF EXISTS sp_GetAllExpeditionsFromACursus
GO

CREATE PROCEDURE sp_GetAllExpeditionsFromACursus
	@CursusId INT
	AS
	SELECT Expeditions.id, Expeditions."Name" FROM Expeditions
	INNER JOIN Cursus ON Cursus.id = Expeditions.FK_Cursus
	WHERE Cursus.id = @CursusId
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