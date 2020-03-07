using System;

namespace WCS
{
    public class Event
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; private set; }

        public Event(String description, DateTime startTime, DateTime endTime)
        {
            Description = description;
            StartTime = startTime;
            EndTime = endTime;
        }

        public void Postpone(TimeSpan timeDelta)
        {
            StartTime = StartTime + timeDelta;
            EndTime = EndTime + timeDelta;
        }
    }
}