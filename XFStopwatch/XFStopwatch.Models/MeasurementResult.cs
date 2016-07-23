using System;
using System.Collections.Generic;
using System.Linq;

namespace XFStopwatch.Models
{
    /// <summary>
    /// 計測結果
    /// </summary>
    public class MeasurementResult
    {
        public DateTime BeginDateTime { get; }
        public TimeSpan ElapsedTime { get; set; }

        public IReadOnlyList<TimeSpan> LapTimes { get; }

        public MeasurementResult(DateTime beginDateTime, TimeSpan elapsedTime, ICollection<TimeSpan> lapTimes)
        {
            BeginDateTime = beginDateTime;
            ElapsedTime = elapsedTime;
            LapTimes = lapTimes.ToList();
        }
    }
}