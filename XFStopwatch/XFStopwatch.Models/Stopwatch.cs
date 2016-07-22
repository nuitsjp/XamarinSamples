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
        private readonly ITimeService _timeService;
        private readonly ITimerService _timerService;
        private readonly ObservableCollection<TimeSpan> _rapTimes = new ObservableCollection<TimeSpan>();
        public DateTime BeginDateTime { get; private set; }
        public DateTime RestartDateTime { get; private set; }
        public TimeSpan Elapsed { get; private set; } = TimeSpan.Zero;
        public StopwatchStatus Status { get; private set; } = StopwatchStatus.Stoped;

        public ReadOnlyObservableCollection<TimeSpan> RapTimes { get; }
        public event EventHandler ElapsedChanged;
        public event EventHandler StatusChanged;
        public Stopwatch()
        {
            _timeService = ServiceLocator.Locate<ITimeService>();
            _timerService = ServiceLocator.Locate<ITimerService>();
            _timerService.Elapsed += (sender, e) =>
            {
                Elapsed = _timeService.Now - BeginDateTime;
                RiseElapsedChanged();
            };
            RapTimes = new ReadOnlyObservableCollection<TimeSpan>(_rapTimes);
        }


        public void Start()
        {
            Status = StopwatchStatus.Running;
            Elapsed = TimeSpan.Zero;
            BeginDateTime = _timeService.Now;
            RestartDateTime = BeginDateTime;
            RiseElapsedChanged();
            _timerService.Start();
        }

        public void Rap()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            _timerService.Stop();
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
