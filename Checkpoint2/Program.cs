using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace WCS
{
    class Program
    {
        public static void Main()
        {
            Event newEvent = new Event("Important meeting", DateTime.Now, DateTime.Now);
            newEvent.StartTime = DateTime.Now;
            newEvent.EndTime = DateTime.Now + TimeSpan.FromHours(1);
            newEvent.Postpone(TimeSpan.FromHours(1));
            Console.WriteLine("Another meeting is postponed");



            // Affiche tous les événements d'une personne
            string date = "20200529";
            string date2 = "20200530";


            DateTime startDate = DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            DateTime endDate = DateTime.ParseExact(date2, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            int personId = 3;

            Database.GetInstance().GetEventFromPerson(personId, startDate, endDate);


            // Affiche toutes les quêtes d'un cursus
            string cursusName = "Cursus 1";
            List<Quest> quests = Database.GetInstance().GetQuestFromCursus(cursusName);

            Console.Write("Quest title \t Quest text");
            foreach(Quest quest in quests)
            {
                Console.WriteLine();
                Console.Write(quest.Title + "\t");
                Console.Write(quest.Text);
            }


            /*
            Console.WriteLine();
            Console.WriteLine("Get all students from a cursus");
            string cursusName = "Cursus 1";
            Database.GetInstance().GetStudentsFromCursus(cursusName);*/
            
        }
    }
}
