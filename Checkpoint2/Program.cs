using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class Program
    {
        public static void Main()
        {
            /*Event newEvent = new Event("Important meeting");
            newEvent.StartTime = DateTime.Now;
            newEvent.EndTime = DateTime.Now + TimeSpan.FromHours(1);
            newEvent.Postpone(TimeSpan.FromHours(1));
            Console.WriteLine("Another meeting is postponed");


            string firstName = "toto";
            string lastName = "Priori";
            DateTime birthday = DateTime.Today;
            Adress adress = new Adress(3, "street", "city", "France");
            string email = "coucou@couou";

            Event event1 = new Event("event1");
            List<Event> events = new List<Event>();
            Agenda agenda = new Agenda(events);
            List<Agenda> agendas = new List<Agenda>();
            agendas.Add(agenda);
            Calendar calendar = new Calendar(agendas);

            Quest quest = new Quest("Title1", "text");
            List<Quest> quests = new List<Quest>();
            quests.Add(quest);
            Expedition expedition = new Expedition("Expedition1", quests, event1);
            List<Expedition> expeditions = new List<Expedition>();
            expeditions.Add(expedition);

            Cursus cursus = new Cursus(calendar, "cursus1", event1, expeditions);

            PersonFactory.Create(firstName, lastName, birthday, adress, email, agenda);
*/

            /*DateTime startDate = new DateTime(2020, 05, 29);
            DateTime endDate = new DateTime(2020, 05, 30);

            *//* List<string> liste = new List<string>();
             liste = Database.GetInstance().GetAllFromDB();

             foreach(string item in liste)
             {
                 Console.WriteLine(item);
             }*/

            int startDate = 20200529;
            int endDate = 20200530;
            int person = 3;

            Database.GetInstance().GetEventFromPerson(person, startDate, endDate);
            
        }
    }
}
