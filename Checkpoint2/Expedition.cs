using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class Expedition
    {
        public string Name { get; private set; }
        public List<Quest> Quests { get; private set; }
        public Event Period { get; private set; }

        public Expedition(string name, List<Quest> quests, Event period)
        {
            Name = name;
            Quests = quests;
            Period = period;
        }
    }
}
