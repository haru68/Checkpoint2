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

        
        public List<AbstractPerson> DisplayAllStudentsFromACursusId(int cursusId)
        {
            List<AbstractPerson> studentsOfASameCursus = new List<AbstractPerson>();

            SqlCommand cmd = new SqlCommand("sp_DisplayAllStudentsFromACursus", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CursusId", SqlDbType.VarChar).Value = cursusId;

            Cursus cursus = Database.GetInstance().GetCursusFromId(cursusId);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int personId = reader.GetInt32(0);
                string personFirstName = reader.GetString(1);
                string personLastName = reader.GetString(2);
                string personEmail = reader.GetString(3);
                DateTime personBirthday = reader.GetDateTime(4);
                int streetNumber = reader.GetInt32(5);
                string streetName = reader.GetString(6);
                string city = reader.GetString(7);
                string country = reader.GetString(8);


                Adress adress = new Adress(streetNumber, streetName, city, country);
                AbstractPerson person = PersonFactory.Create(personId, personFirstName, personLastName, personBirthday, adress, personEmail, cursus);
                studentsOfASameCursus.Add(person);
            }
            reader.Close();


            return studentsOfASameCursus;
        }

        public Cursus GetCursusFromId(int cursusId)
        {
            SqlCommand cmd = new SqlCommand("sp_GetCursusFromId", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CursusId", SqlDbType.VarChar).Value = cursusId;
            string name = "";
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                name = reader.GetString(0);
                startDate = reader.GetDateTime(1);
                endDate = reader.GetDateTime(2);

            }
            reader.Close();

            List<Expedition> expeditions = Database.GetInstance().GetAllExpeditionsFromCursusId(cursusId);
            Calendar calendar = Database.GetInstance().GetCalendarFromCursusId(cursusId);
            Cursus cursus = new Cursus(calendar, name, startDate, endDate, expeditions);

            return cursus;
        }

        public Calendar GetCalendarFromCursusId (int cursusId)
        {
            Agenda agenda = new Agenda(Database.GetInstance().GetAllEventsFromCursusId(cursusId));
            List<Agenda> agendas = new List<Agenda>();
            agendas.Add(agenda);

            SqlCommand cmd = new SqlCommand("sp_GetAllCalendarFromCursusId", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CursusId", SqlDbType.VarChar).Value = cursusId;
            string name = "";
            string description = "";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                name = reader.GetString(1);
                description = reader.GetString(2);
            }
            reader.Close();

            Calendar calendar = new Calendar(agendas, name, description);
            return calendar;
        }

        public List<Event> GetAllEventsFromCursusId(int cursusId)
        {
            List<Event> events = new List<Event>();

            SqlCommand cmd = new SqlCommand("sp_GetAllEventsFromCursusId", Connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CursusId", SqlDbType.VarChar).Value = cursusId;

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string agendaDescription = reader.GetString(0);
                DateTime startDate = reader.GetDateTime(1);
                DateTime endDate = reader.GetDateTime(2);

                Event evente = new Event(agendaDescription, startDate, endDate);

                events.Add(evente);
            }
            reader.Close();

            return events;
        }

        public void DisplayAllEventFromPerson(int personId, DateTime startDate, DateTime endDate)
        {
            SqlCommand command = new SqlCommand("sp_DisplayAllEventFromUserByDate", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@PersonId", SqlDbType.Int)).Value = personId;
            command.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.Date)).Value = startDate;
            command.Parameters.Add(new SqlParameter("@EndDate", SqlDbType.Date)).Value = endDate;

            Console.WriteLine("Calendar name \t Agenda id \t Event Description \t Start date \t End date \t");

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.Write(reader.GetString(0) + "\t" + reader.GetInt32(1) + "\t" + reader.GetString(2) + "\t" + reader.GetDateTime(3) + "\t" + reader.GetDateTime(4));
                Console.WriteLine();
            }
            reader.Close();

        }

        public void DisplayAllQuestsFromACursus(int cursusId)
        {
            SqlCommand command = new SqlCommand("sp_DisplayAllQuestsFromACursus", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@CursusId", SqlDbType.Int)).Value = cursusId;
            

            Console.WriteLine("Quest id \t quest name \t quest text \t expedition name");

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.Write(reader.GetInt32(0) + "\t" + reader.GetString(1) + "\t" + reader.GetString(2) + "\t" + reader.GetString(3));
                Console.WriteLine();
                    
            }
            reader.Close();

        }

        public List<Quest> GetAllQuestsFromCursusId(int cursusId)
        {
            List<Quest> quests = new List<Quest>();

            SqlCommand command = new SqlCommand("sp_GetAllQuestsFromACursus", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@CursusId", SqlDbType.Int)).Value = cursusId;

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                string title = reader.GetString(0);
                string text = reader.GetString(1);
                Quest quest = new Quest(title, text);
                quests.Add(quest);
            }
            reader.Close();

            return quests;
        }

        public List<Expedition> GetAllExpeditionsFromCursusId(int cursusId)
        {
            List<Expedition> expeditions = new List<Expedition>();
            List<int> expeditionsId = new List<int>();
            List<string> expeditionsName = new List<string>();

            SqlCommand command = new SqlCommand("sp_GetAllExpeditionsFromACursus", Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("@CursusId", SqlDbType.Int)).Value = cursusId;

            SqlDataReader reader = command.ExecuteReader();
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

    }
}
