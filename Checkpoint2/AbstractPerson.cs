using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class AbstractPerson
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime Birthday { get; private set; }
        public Adress LivingLocation { get; private set; }
        public string Email { get; private set; }
        public Agenda Agenda {get; private set;}


        public AbstractPerson(string firstName, string lastName, DateTime birthday, Adress adress, string email, Agenda agenda)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            LivingLocation = adress;
            Email = email;
            Agenda = agenda;
        }
    }
}
