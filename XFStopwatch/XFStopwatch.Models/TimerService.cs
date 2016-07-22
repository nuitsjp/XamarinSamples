using System;
using System.Threading;
using System.Threading.Tasks;

namespace XFStopwatch.Models
{
    public class TimerService : ITimerService
    {
        private CancellationTokenSource _cancellationTokenSource;
        public TimeSpan Interval { get; set; }

        public event EventHandler Elapsed;

        public void Start()
        {
            if(Interval == default(TimeSpan))
                throw new InvalidOperationException(nameof(Interval));

            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    await Task.Delay(Interval);
                    if (_cancellationTokenSource != null)
                    {
                        Elapsed?.Invoke(this, EventArgs.Empty);
                    }
                }
            }, _cancellationTokenSource.Token);
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;
        }
    }
}