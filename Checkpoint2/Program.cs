using System;
using System.Collections.Generic;
using System.Text;

namespace WCS
{
    class Program
    {
        public static void Main()
        {
            Event newEvent = new Event("Important meeting");
            newEvent.StartTime = DateTime.Now;
            newEvent.EndTime = DateTime.Now + TimeSpan.FromHours(1);
            newEvent.Postpone(TimeSpan.FromHours(1));
            Console.WriteLine("Another meeting is postponed");
        }
    }
}
