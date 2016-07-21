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
        private DateTime _beginDateTime;
        private readonly ObservableCollection<TimeSpan> _rapTimes = new ObservableCollection<TimeSpan>();
        public TimeSpan Elapsed { get; private set; } = TimeSpan.Zero;
        public StopwatchStatus Status { get; private set; } = StopwatchStatus.Stoped;

        public ReadOnlyObservableCollection<TimeSpan> RapTimes { get; }
        public event EventHandler ElapsedChanged;
        public event EventHandler StatusChanged;
        public Stopwatch(ITimeService timeService, ITimerService timerService)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            _timeService = timeService;
            _timerService = timerService;
            _timerService.Elapsed += (sender, e) =>
            {
                Elapsed = _timeService.Now - _beginDateTime;
                RiseElapsedChanged();
            };
            RapTimes = new ReadOnlyObservableCollection<TimeSpan>(_rapTimes);
        }


        public void Start()
        {
            Status = StopwatchStatus.Running;
            Elapsed = TimeSpan.Zero;
            _beginDateTime = DateTime.Now;
            RiseElapsedChanged();
            _timerService.Start();
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

        private void RiseElapsedChanged()
        {
            ElapsedChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
