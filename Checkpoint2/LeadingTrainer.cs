using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class LeadingTrainer : AbstractPerson
    {
        public List<Trainer> Trainers { get; private set; }

        public LeadingTrainer(int personId, string firstName, string lastName, DateTime birthday, Adress adress, string email, Agenda agenda, List<Trainer> trainers) : base(personId, firstName, lastName, birthday, adress, email, agenda)
        {
            Trainers = trainers;
        }
    }
}
