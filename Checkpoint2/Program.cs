using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using CommandLine;


namespace WCS
{
    class Program
    {
        [Verb("event", HelpText = "Get all event from a person id into a time interval")]
        class EventFromPersonOptions
        {
            [Option('i', "personId", Required = true, HelpText = "Person id (1 to 225)")]
            public int PersonId { get; set; }

            [Option('s', "startDate", Required = true, HelpText = "Event startDate format yyyyMMdd (20200529)")]
            public string StartDate { get; set; }

            [Option('e', "endDate", Required = true, HelpText = "Event end Date format yyyyMMdd (20200530)")]
            public string EndDate { get; set; }

        }

        [Verb("quests", HelpText = "Get all quests from a Cursus")]
        class QuestFromCursus
        {
            [Option('n', "cursusId", Required = true, HelpText = "Id of the cursus")]
            public int CursusId { get; set; }
        }

        [Verb("students", HelpText = "Get all students from a Cursus")]
        class StudentsFromCursus
        {
            [Option('n', "cursusId", Required = true, HelpText = "Id of the cursus")]
            public int CursusId { get; set; }
        }


        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<EventFromPersonOptions, QuestFromCursus, StudentsFromCursus>(args)
                            .WithParsed<EventFromPersonOptions>(RunEventFromPersonOption)
                            .WithParsed<QuestFromCursus>(RunQuestFromCursus)
                            .WithParsed<StudentsFromCursus>(RunStudentsFromCursus);

            //Database.GetInstance().DisplayAllQuestsFromACursus(1);

            /*Event newEvent = new Event("Important meeting", DateTime.Now, DateTime.Now);
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
            }*/


            /*
            Console.WriteLine();
            Console.WriteLine("Get all students from a cursus");
            string cursusName = "Cursus 1";
            Database.GetInstance().GetStudentsFromCursus(cursusName);*/

        }

        static void RunEventFromPersonOption(EventFromPersonOptions options)
        {
            DateTime startDate = DateTime.ParseExact(options.StartDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            DateTime endDate = DateTime.ParseExact(options.EndDate, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None);
            Database.GetInstance().DisplayAllEventFromPerson(options.PersonId, startDate, endDate);
        }

        static void RunQuestFromCursus(QuestFromCursus options)
        {
            List<Quest> quests = Database.GetInstance().GetAllQuestsFromCursusId(options.CursusId);

            Console.Write("Quest title \t Quest text");
            foreach (Quest quest in quests)
            {
                Console.WriteLine();
                Console.Write(quest.Title + "\t");
                Console.Write(quest.Text);
            }
        }

        static void RunStudentsFromCursus(StudentsFromCursus options)
        {
            List<AbstractPerson> students = Database.GetInstance().DisplayAllStudentsFromACursusId(options.CursusId);

            Console.WriteLine("FirstName \t LastName \t Birthday \t email");
            foreach(AbstractPerson student in students)
            {
                Console.Write(student.FirstName + "\t" + student.LastName + "\t" + student.Birthday + "\t" + student.Email);
                Console.WriteLine();
            }
        }
    }
}
