using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class Student : AbstractPerson
    {
        public Cursus FollowedCurses { get; set; }

        public Student(string firstName, string lastName, DateTime birthday, Adress adress, string email, Agenda agenda, Cursus followedCursus) : base (firstName, lastName, birthday, adress, email, agenda)
        {
            FollowedCurses = followedCursus;
        }
    }
}
