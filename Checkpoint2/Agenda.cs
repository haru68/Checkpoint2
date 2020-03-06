using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class Agenda
    {
        public List<Event> EventList { get; set; }

        public Agenda(List<Event> events)
        {
            EventList = events;
        }
    }
}
