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
        /// 経過時間を取得する
        /// </summary>
        TimeSpan Elapsed { get; }
        /// <summary>
        /// 状態を取得する
        /// </summary>
        StopwatchStatus Status { get; }
        /// <summary>
        /// ラップタイムの一覧を取得する
        /// </summary>
        ReadOnlyObservableCollection<TimeSpan> RapTimes { get; }
        /// <summary>
        /// 経過時間更新イベント
        /// </summary>
        event EventHandler<SingleParameterEventArgs<TimeSpan>> ElapsedChanged;
        /// <summary>
        /// 状態更新イベント
        /// </summary>
        event EventHandler<SingleParameterEventArgs<StopwatchStatus>> StatusChanged;
        /// <summary>
        /// 計測を開始する
        /// </summary>
        /// <remarks>
        /// <see cref="Status"/>が停止状態の場合に計測を開始する。
        /// それ以外の状態の場合、何も行わない。
        /// </remarks>
        void Start();
        /// <summary>
        /// ラップタイムを取得する
        /// </summary>
        /// <remarks>
        /// <see cref="Status"/>が計測中状態の場合にラップタイムを取得する。
        /// それ以外の状態の場合、何も行わない。
        /// </remarks>
        void Rap();
        /// <summary>
        /// 計測を一時停止状態へ変更する
        /// </summary>
        /// <remarks>
        /// <see cref="Status"/>が計測中状態の場合に一時停止状態へ変更する。
        /// それ以外の状態の場合、何も行わない。
        /// </remarks>
        void Stop();
        /// <summary>
        /// 計測を停止する
        /// </summary>
        /// <remarks>
        /// <see cref="Status"/>が一時停止状態の場合に計測を停止する。
        /// それ以外の状態の場合、何も行わない。
        /// </remarks>
        void Reset();
    }
}