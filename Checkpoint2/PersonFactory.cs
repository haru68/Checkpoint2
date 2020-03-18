using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace WCS
{
    public static class PersonFactory
    {

        
        public static AbstractPerson Create(List<AbstractPerson> people = null)
        {
            AbstractPerson person;


            if (people != null && people.Any(s => s.GetType() == typeof(Trainer)))
            {
                person = new Trainer { Subordinates = people, IsLead = true };

            }
            else if (people != null && people.Count > 0)
            {
                person = new Trainer { Subordinates = people, IsLead = false };
            }
            else
            {
                person = new Student { Subordinates = new List<AbstractPerson>(), IsLead = false};
            }
            return person;
        }

    }
}
