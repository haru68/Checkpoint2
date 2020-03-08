﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class Student : AbstractPerson
    {
        public Cursus FollowedCurses { get; set; }

        public Student(int personId, string firstName, string lastName, DateTime birthday, Adress adress, string email, Cursus followedCursus) : base (personId, firstName, lastName, birthday, adress, email)
        {
            FollowedCurses = followedCursus;
        }
    }
}
