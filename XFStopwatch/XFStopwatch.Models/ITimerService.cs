using System;

namespace XFStopwatch.Models
{
    /// <summary>
    /// 一定間隔を通知するサービス
    /// </summary>
    public interface ITimerService
    {
        /// <summary>
        /// 通知感覚を取得・設定する
        /// </summary>
        TimeSpan Interval { get; set; }
        /// <summary>
        /// 時間経過イベント
        /// </summary>
        event EventHandler Elapsed;
        /// <summary>
        /// 通知開始
        /// </summary>
        void Start();
        /// <summary>
        /// 通知停止
        /// </summary>
        void Stop();

    }
}
