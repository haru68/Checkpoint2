using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public class Expedition
    {
        public string Name { get; private set; }
        public List<Quest> Quests { get; private set; }

        public Expedition(string name, List<Quest> quests)
        {
            Name = name;
            Quests = quests;
        }
    }
}
