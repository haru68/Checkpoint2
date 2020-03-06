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
	SELECT Calendar."Name", Agenda."Description", Agenda.StartTime, Agenda.EndTime FROM Agenda
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
	/*@PersonId INT,
	@StartDate DATETIME,
	@EndDate DATETIME*/
	AS
	SELECT Calendar."Name", Agenda."Description", Agenda.StartTime, Agenda.EndTime FROM Agenda
	INNER JOIN Calendar ON Agenda.FK_Calendar = Calendar.id
	INNER JOIN Person ON Calendar.id = Person.FK_Calendar
	WHERE Person.id = @PersonId AND Agenda.StartTime = @StartDate AND Agenda.EndTime = @EndDate
	RETURN
	GO

SELECT * FROM Agenda
GO

EXECUTE sp_GetAllEventFromUserByDate 3, '2020-29-05', '2020-30-05'
GO

--CREATE PROCEDURE sp_GetAllEventFromUser
