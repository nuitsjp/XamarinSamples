using System;
using System.Collections.ObjectModel;

namespace XFStopwatch.Models
{
    /// <summary>
    /// ストップウォッチモデルのインターフェース
    /// </summary>
    public interface IStopwatch
    {
        /// <summary>
        /// 計測開始日時を取得する
        /// </summary>
        DateTime BeginDateTime { get; }
        /// <summary>
        /// 経過時間を取得する
        /// </summary>
        TimeSpan ElapsedTime { get; }
        /// <summary>
        /// 状態を取得する
        /// </summary>
        StopwatchStatus Status { get; }
        /// <summary>
        /// ラップタイムの一覧を取得する
        /// </summary>
        ReadOnlyObservableCollection<TimeSpan> LapTimes { get; }
        /// <summary>
        /// 計測結果履歴を取得する
        /// </summary>
        ReadOnlyObservableCollection<MeasurementResult> MeasurementResults { get; }
        /// <summary>
        /// 経過時間更新イベント
        /// </summary>
        event EventHandler ElapsedTimeChanged;
        /// <summary>
        /// 状態更新イベント
        /// </summary>
        event EventHandler StatusChanged;
        /// <summary>
        /// 計測を開始する
        /// </summary>
        void Start();
        /// <summary>
        /// ラップタイムを取得する
        /// </summary>
        void Lap();
        /// <summary>
        /// 計測を一時停止状態へ変更する
        /// </summary>
        void Pause();
        /// <summary>
        /// 計測をリセットし、計測結果を履歴として保存する
        /// </summary>
        void Reset();
    }
}