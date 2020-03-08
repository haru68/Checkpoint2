using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public abstract class AbstractPerson
    {
        public int PersonId { get; private set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public DateTime Birthday { get; protected set; }
        public Adress LivingLocation { get; protected set; }
        public string Email { get; protected set; }


        public AbstractPerson(int personId, string firstName, string lastName, DateTime birthday, Adress adress, string email)
        {
            PersonId = personId;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            LivingLocation = adress;
            Email = email;
        }
        
    }
}
