using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public class Cursus
    {
        public Calendar CursusCalendar { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public List<Expedition> Expeditions { get; private set; }

        public Cursus(Calendar calendar, string name, DateTime startDate, DateTime date, List<Expedition> expeditions)
        {
            CursusCalendar = calendar;
            Name = name;
            StartDate = startDate;
            EndDate = EndDate;
            Expeditions = expeditions;
        }
    }
}
