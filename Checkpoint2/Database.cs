using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WCS
{
    class Database
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
/*
            SqlParameter PersonId = command.Parameters.Add("@PersonId", SqlDbType.Int, personId);
            PersonId.Direction = ParameterDirection.Input;
            SqlParameter StartDate = command.Parameters.Add("@StartDate", SqlDbType.Date, startDate);
            StartDate.Direction = ParameterDirection.Input;
            SqlParameter EndDate = command.Parameters.Add("@EndDate", SqlDbType.Date, endDate);
            EndDate.Direction = ParameterDirection.Input;*/

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("event" + reader.GetString(1));
            }
            reader.Close();


            /*
            SqlCommand cmd = new SqlCommand("sp_GetAllEventFromUserByDate", Connection);
            cmd.Connection = Connection;
            cmd.Parameters.Add("@PersonId", SqlDbType.Int).Value = personId;
            cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.Date).Value = endDate;
            cmd.ExecuteNonQuery();*/

        }

        public List<string> GetAllFromDB()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Connection;
            cmd.CommandText = "SELECT \'FirstName\' FROM Person";
            List<string> groceryName = new List<string>();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        groceryName.Add(Convert.ToString(reader));
                    }
                }
            }
            return groceryName;

        }
    }
}
