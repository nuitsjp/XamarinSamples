using System;

namespace XFStopwatch.Models
{
    public class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
