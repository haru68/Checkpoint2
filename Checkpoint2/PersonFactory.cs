using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    static class PersonFactory
    {

        public static AbstractPerson Create(int personId, string firstName, string lastName, DateTime birthday, Adress adress, string email, Agenda agenda, Cursus cursus = null, List<Student> students = null, List<Trainer> trainers = null)
        {
            AbstractPerson person;
            if (students == null)
            {
                person = new Student(personId, firstName, lastName, birthday, adress, email, agenda, cursus);
            }
            else if (students != null)
            {
                person = new Trainer(personId, firstName, lastName, birthday, adress, email, agenda, students, cursus);
            }
            else if (trainers != null)
            {
                person = new LeadingTrainer(personId, firstName, lastName, birthday, adress, email, agenda, trainers);
            }
            else
            {
                throw new ArgumentException("There are no person fitting the desired parameters.");
            }

            return person;

        }
    }
}
