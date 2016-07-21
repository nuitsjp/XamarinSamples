using System;

namespace XFStopwatch.Models
{
    public interface ITimeService
    {
        DateTime Now { get; }
    }
}