using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class LeadingTrainer : AbstractPerson
    {
        public List<Trainer> Trainers { get; private set; }
        public Cursus TrainedCursus { get; private set; }

        public LeadingTrainer(int personId, string firstName, string lastName, DateTime birthday, Adress adress, string email, List<Trainer> trainers, Cursus cursus) : base(personId, firstName, lastName, birthday, adress, email)
        {
            Trainers = trainers;
            TrainedCursus = cursus;
        }
    }
}
