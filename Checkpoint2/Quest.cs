using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    public class Quest
    {
        public string Title { get; private set; }
        public string Text { get; private set; }

        public Quest(string title, string text)
        {
            Title = title;
            Text = text;
        }
    }
}
