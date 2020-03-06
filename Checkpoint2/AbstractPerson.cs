using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public abstract class AbstractPerson
    {
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime Birthday { get; protected set; }
        public Adress LivingLocation { get; protected set; }
        public string Email { get; protected set; }
        public Agenda Agenda {get; protected set;}


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
