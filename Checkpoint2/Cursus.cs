using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class Cursus
    {
        public Calendar CursusCalendar { get; private set; }
        public string Name { get; private set; }
        public Event Dates { get; private set; }
        public List<Expedition> Expeditions { get; private set; }

        public Cursus(Calendar calendar, string name, Event dates, List<Expedition> expeditions)
        {
            CursusCalendar = calendar;
            Name = name;
            Dates = dates;
            Expeditions = expeditions;
        }
    }
}
