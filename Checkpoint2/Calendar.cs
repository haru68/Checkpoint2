using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public class Calendar
    {
        public List<Agenda> Agendas { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Calendar(List<Agenda> agendas, string name, string description)
        {
            Agendas = agendas;
            Name = name;
            Description = description;
        }
    }
}
