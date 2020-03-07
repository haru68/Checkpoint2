using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class Trainer : AbstractPerson
    {
        public List<Student> Students { get; private set; }
        public Cursus TrainedCursus { get; private set; }

        public Trainer(int personId, string firstName, string lastName, DateTime birthday, Adress adress, string email, Agenda agenda, List<Student> students, Cursus trainedCursus) : base(personId, firstName, lastName, birthday, adress, email, agenda)
        {
            Students = students;
            TrainedCursus = trainedCursus;
        }
    }
}
