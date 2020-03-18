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
        public List<AbstractPerson> Subordinates { get; set; }
        public Boolean IsLead { get; set; }



    }
}
