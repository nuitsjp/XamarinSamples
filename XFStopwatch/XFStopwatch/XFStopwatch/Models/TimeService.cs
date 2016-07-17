using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFStopwatch.Models
{
    public class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;
    }
}
