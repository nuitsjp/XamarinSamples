using System;

namespace XFStopwatch.Models
{
    /// <summary>
    /// 時間サービス
    /// </summary>
    public interface ITimeService
    {
        /// <summary>
        /// 現在日時を取得する
        /// </summary>
        DateTime Now { get; }
    }
}