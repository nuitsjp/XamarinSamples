using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFStopwatch.Models
{
    public class Stopwatch : IStopwatch
    {
        private ITimeService _timeService;
        private ITimerService _timerService;
        private readonly ObservableCollection<TimeSpan> _rapTimes = new ObservableCollection<TimeSpan>();
        public TimeSpan Elapsed { get; } = TimeSpan.Zero;
        public StopwatchStatus Status { get; } = StopwatchStatus.Stoped;

        public ReadOnlyObservableCollection<TimeSpan> RapTimes { get; }
        public event EventHandler<SingleParameterEventArgs<TimeSpan>> ElapsedChanged;
        public event EventHandler<SingleParameterEventArgs<StopwatchStatus>> StatusChanged;
        public Stopwatch(ITimeService timeService, ITimerService timerService)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            _timeService = timeService;
            _timerService = timerService;
            RapTimes = new ReadOnlyObservableCollection<TimeSpan>(_rapTimes);
        }


        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Rap()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
