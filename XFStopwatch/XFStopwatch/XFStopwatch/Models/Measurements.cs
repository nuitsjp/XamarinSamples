using System;
using System.Collections.Generic;
using System.Linq;

namespace XFStopwatch.Models
{
    /// <summary>
    /// 計測結果
    /// </summary>
    public class Measurements
    {
        public DateTime BeginDateTime { get; }
        public TimeSpan ElapsedTime { get; set; }

        public IReadOnlyList<TimeSpan> RapTimes { get; }

        public Measurements(DateTime beginDateTime, TimeSpan elapsedTime, ICollection<TimeSpan> rapTimes)
        {
            BeginDateTime = beginDateTime;
            ElapsedTime = elapsedTime;
            RapTimes = rapTimes.ToList();
        }
    }
}