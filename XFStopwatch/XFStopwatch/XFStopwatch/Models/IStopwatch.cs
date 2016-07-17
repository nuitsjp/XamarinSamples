using System;
using System.Collections.ObjectModel;

namespace XFStopwatch.Models
{
    public interface IStopwatch
    {
        TimeSpan Elapsed { get; }
        StopwatchStatus Status { get; }
        ReadOnlyObservableCollection<TimeSpan> RapTimes { get; }

        event EventHandler<SingleParameterEventArgs<TimeSpan>> ElapsedChanged;
        event EventHandler<SingleParameterEventArgs<StopwatchStatus>> StatusChanged;
        void Start();
        void Rap();
        void Stop();
        void Reset();
    }
}