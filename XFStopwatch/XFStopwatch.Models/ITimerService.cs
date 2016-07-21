using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
