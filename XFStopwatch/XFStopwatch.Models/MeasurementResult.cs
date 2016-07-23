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
        /// <summary>
        /// 計測開始日時を取得する
        /// </summary>
        public DateTime BeginDateTime { get; }
        /// <summary>
        /// 総経過時間を取得する
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }
        /// <summary>
        /// ラップタイムを取得する
        /// </summary>
        public IReadOnlyList<TimeSpan> LapTimes { get; }
        /// <summary>
        /// インスタンスを初期化する
        /// </summary>
        /// <param name="beginDateTime"></param>
        /// <param name="elapsedTime"></param>
        /// <param name="lapTimes"></param>
        public MeasurementResult(DateTime beginDateTime, TimeSpan elapsedTime, ICollection<TimeSpan> lapTimes)
        {
            BeginDateTime = beginDateTime;
            ElapsedTime = elapsedTime;
            LapTimes = lapTimes.ToList();
        }
    }
}