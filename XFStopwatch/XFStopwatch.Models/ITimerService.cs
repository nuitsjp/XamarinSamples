using System;

namespace XFStopwatch.Models
{
    public interface ITimerService
    {
        TimeSpan Interval { get; set; }

        event EventHandler Elapsed;

        void Start();

        void Stop();

    }
}
