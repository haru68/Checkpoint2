using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class Calendar
    {
        public List<Agenda> Agendas { get; private set; }

        public Calendar(List<Agenda> agendas)
        {
            Agendas = agendas;
        }
    }
}
