using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WCS
{
    public class Database
    {
        private SqlConnection Connection { get; set; }
        private static Database singleton = null;

        private Database()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.ConnectionString = @"Data Source=PC-Haru\SQLEXPRESS;Initial Catalog=WCS_School;Integrated Security=True";
            Connection = new SqlConnection(builder.ConnectionString);
            Connection.Open();
        }

        public static Database GetInstance()
        {
            if (singleton == null)
            {
                singleton = new Database();
            }
            return singleton;
        }

        public void GetEventFromPerson(int personId, DateTime startDate, DateTime endDate)
        {
            SqlCommand command = new SqlCommand("sp_GetAllEventFromUserByDate", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int)).Value = personId;
            command.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date)).Value = startDate;
            command.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date)).Value = endDate;

            Console.WriteLine("Calendar name \t Agenda id \t Event Description \t Start date \t End date \t");

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.Write(reader.GetString(0));
                Console.Write("\t");
                Console.Write(reader.GetInt32(1));
                Console.Write("\t");
                Console.Write(reader.GetString(2));
                Console.Write("\t");
                Console.Write(reader.GetDateTime(3));
                Console.Write("\t");
                Console.Write(reader.GetDateTime(4));

                Console.WriteLine();
            }
            reader.Close();
        }

        public List<AbstractPerson> GetStudentsFromCursus(string cursusName)
        {
            Cursus cursus = Database.GetInstance().GetCursusFromName(cursusName);
            List<AbstractPerson> studentsOfASameCursus = new List<AbstractPerson>();

            SqlCommand cmd = new SqlCommand("sp_GetAllStudentsInACursus", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CursusName", SqlDbType.VarChar).Value = cursusName;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int personId = reader.GetInt32(0);
                string firstName = reader.GetString(1);
                string lastName = reader.GetString(2);
                DateTime birthday = reader.GetDateTime(3);
                string email = reader.GetString(4);
                reader.Close();

                Adress adress = Database.GetInstance().GetAdressFromPersonId(personId);
                Agenda agenda = Database.GetInstance().GetAgendaFromPersonId(personId);

                AbstractPerson student = PersonFactory.Create(personId, firstName, lastName, birthday, adress, email, agenda);
                studentsOfASameCursus.Add(student);
                reader.Read();
            }
            reader.Close();
            return studentsOfASameCursus;
        }

        public Cursus GetCursusFromName(string cursusName)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            List<Expedition> expeditions = new List<Expedition>();
            Calendar calendar;

            Cursus cursus;

            SqlCommand cmd = new SqlCommand("sp_GetCursus", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CursusName", SqlDbType.VarChar).Value = cursusName;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                startDate = reader.GetDateTime(0);
                endDate = reader.GetDateTime(1);
            }
            reader.Close();

            expeditions = Database.GetInstance().GetExpeditionFromCursus(cursusName);
            calendar = Database.GetInstance().GetCalendarFromCursusName(cursusName);
            cursus = new Cursus(calendar, cursusName, startDate, endDate, expeditions);

            return cursus;
        }

        public List<Quest> GetQuestFromCursus(string cursusName)
        {
            List<Quest> quests = new List<Quest>();
            SqlCommand cmd = new SqlCommand("sp_GetQuestForACursus", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CursusName", SqlDbType.VarChar).Value = cursusName;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string questName = reader.GetString(1);
                string questText = reader.GetString(2);
                Quest quest = new Quest(questName, questText);

                quests.Add(quest);
            }
            reader.Close();

            return quests;
        }

        public List<Expedition> GetExpeditionFromCursus(string cursusName)
        {
            List<Expedition> expeditions = new List<Expedition>();
            SqlCommand cmd = new SqlCommand("sp_GetExpeditionsForACursus", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CursusName", SqlDbType.VarChar).Value = cursusName;

            List<int> expeditionsId = new List<int>();
            List<string> expeditionsName = new List<string>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int expeditionId = reader.GetInt32(0);
                string expeditionName = reader.GetString(1);

                expeditionsId.Add(expeditionId);
                expeditionsName.Add(expeditionName);
            }
            reader.Close();

            List<Quest> quests = new List<Quest>();
            int counterId = 0;
            foreach (string name in expeditionsName)
            {
                quests = Database.GetInstance().GetQuestsFromExpeditionId(expeditionsId[counterId]);
                counterId++;
                Expedition expedition = new Expedition(name, quests);
                expeditions.Add(expedition);
            }
            return expeditions;
        }

        public List<Quest> GetQuestsFromExpeditionId(int expeditionId)
        {
            SqlCommand cmd = new SqlCommand("sp_GetQuestsFromExpeditionId", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ExpeditionId", SqlDbType.VarChar).Value = expeditionId;

            List<Quest> quests = new List<Quest>();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string questTitle = reader.GetString(1);
                string questText = reader.GetString(2);
                Quest quest = new Quest(questTitle, questText);
                quests.Add(quest);
            }
            reader.Close();
            return quests;
        }


        public List<Agenda> GetAgendaFromCursusName(string cursusName)
        {
            List<Event> events = new List<Event>();
            List<Agenda> agendas = new List<Agenda>();

            SqlCommand cmd = new SqlCommand("sp_GetAgendaForACursusFromCursusName", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CursusName", SqlDbType.VarChar).Value = cursusName;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string description = reader.GetString(0);
                DateTime startDate = reader.GetDateTime(1);
                DateTime endDate = reader.GetDateTime(2);
                Event evente = new Event(description, startDate, endDate);

                events.Add(evente);
                Agenda agenda = new Agenda(events);
                agendas.Add(agenda);
            }
            reader.Close();

            return agendas;
        }

        public Calendar GetCalendarFromCursusName(string cursusName)
        {
            List<Agenda> agendas = new List<Agenda>();
            agendas = Database.GetInstance().GetAgendaFromCursusName(cursusName);


            SqlCommand cmd = new SqlCommand("sp_GetCalendarForACursus", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CursusName", SqlDbType.VarChar).Value = cursusName;

            string calendarName = "";
            string calendarDescription = "";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                calendarName = reader.GetString(0);
                calendarDescription = reader.GetString(1);
            }
            reader.Close();

            Calendar calendar = new Calendar(agendas, calendarName, calendarDescription);

            return calendar;
        }

        public Adress GetAdressFromPersonId(int personId)
        {
            int adressId = 0;
            int streetNumber = 0;
            string streetName = "";
            string cityName = "";
            string country = "";
            SqlCommand cmd = new SqlCommand("sp_GetPersonAdressFromId", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PersonId", SqlDbType.VarChar).Value = personId;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                adressId = reader.GetInt32(0);
                streetNumber = reader.GetInt32(1);
                streetName = reader.GetString(2);
                cityName = reader.GetString(3);
                country = reader.GetString(4);

                Adress adress = new Adress(streetNumber, streetName, cityName, country);
                return adress;
            }
            reader.Close();

            throw new ArgumentException("No adress recorded for this person id " + personId);
        }

        public Agenda GetAgendaFromPersonId(int personId)
        {
            List<Event> events = new List<Event>();

            SqlCommand cmd = new SqlCommand("sp_GetPersonAgendaFromPersonId", Connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PersonId", SqlDbType.VarChar).Value = personId;
            
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string description = reader.GetString(1);
                DateTime startDate = reader.GetDateTime(2);
                DateTime endDate = reader.GetDateTime(3);
                Event evente = new Event(description, startDate, endDate);

                events.Add(evente);
            }
            reader.Close();

            Agenda agenda = new Agenda(events);

            return agenda;
        }
    }
}
