using System;
using System.Collections.ObjectModel;

namespace XFStopwatch.Models
{
    public class Stopwatch : IStopwatch
    {
        /// <summary>
        /// 現在時刻を取得するためのサービス
        /// </summary>
        private readonly ITimeService _timeService;
        /// <summary>
        /// 定期実行処理を行うためのタイマーサービス
        /// </summary>
        private readonly ITimerService _timerService;
        /// <summary>
        /// 計測開時刻
        /// </summary>
        /// <remarks>
        /// 開始->停止->開始とした場合に、トータルの経過時刻を計算するために最後に再開（もしくは開始）
        /// した時間を保持しておく
        /// </remarks>
        private DateTime _beginDateTime;
        /// <summary>
        /// 直前のラップタイム計測日時
        /// </summary>
        private DateTime _previousLapDateTime;
        /// <summary>
        /// 一時停止から再開した際の、再開前の経過時間
        /// </summary>
        private TimeSpan _storedTime;
        /// <summary>
        /// トータルの経過時間
        /// </summary>
        private TimeSpan _elapsedTime;
        /// <summary>
        /// 状態
        /// </summary>
        private StopwatchStatus _status = StopwatchStatus.Stoped;
        /// <summary>
        /// ラップタイム
        /// </summary>
        private readonly ObservableCollection<LapTime> _lapTimes = new ObservableCollection<LapTime>();

        /// <summary>
        /// 経過時間を取得・設定する
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get { return _elapsedTime; }
            private set
            {
                if (_elapsedTime != value)
                {
                    _elapsedTime = value;
                    ElapsedTimeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 状態を取得・設定する
        /// </summary>
        public StopwatchStatus Status
        {
            get { return _status; }
            private set
            {
                if (_status != value)
                {
                    _status = value;
                    StatusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// ラップタイムを取得する
        /// </summary>
        public ReadOnlyObservableCollection<LapTime> LapTimes { get; }

        /// <summary>
        /// 経過時間変更イベント
        /// </summary>
        public event EventHandler ElapsedTimeChanged;
        /// <summary>
        /// 状態変更イベント
        /// </summary>
        public event EventHandler StatusChanged;

        /// <summary>
        /// インスタンスを初期化する
        /// </summary>
        public Stopwatch()
        {
            _timeService = ServiceLocator.Locate<ITimeService>();
            _timerService = ServiceLocator.Locate<ITimerService>();
            _timerService.Elapsed += (sender, e) =>
            {
                ElapsedTime = _timeService.Now - _beginDateTime + _storedTime;
            };
            LapTimes = new ReadOnlyObservableCollection<LapTime>(_lapTimes);
        }
        /// <summary>
        /// 計測を開始する
        /// </summary>
        public void Start()
        {
            if (Status == StopwatchStatus.Stoped)
            {
                _beginDateTime = _timeService.Now;
                _previousLapDateTime = _beginDateTime;
                Status = StopwatchStatus.Running;
                _timerService.Start();
            }
            else if(Status == StopwatchStatus.Paused)
            {
                _beginDateTime = _timeService.Now;
                _previousLapDateTime = _beginDateTime;
                Status = StopwatchStatus.Running;
                _timerService.Start();
            }
            else
            {
                throw new InvalidOperationException($"Stopwatch status is {Status}.");
            }
        }

        /// <summary>
        /// ラップタイムを取得する
        /// </summary>
        public void Lap()
        {
            if (Status == StopwatchStatus.Running)
            {
                var now = _timeService.Now;
                var elapsedTime = now - _previousLapDateTime;
                _lapTimes.Add(new LapTime(_lapTimes.Count + 1, elapsedTime));
                _previousLapDateTime = now;
            }
            else
            {
                throw new InvalidOperationException($"Stopwatch status is {Status}.");
            }
        }
        /// <summary>
        /// 計測を一時停止する
        /// </summary>
        public void Pause()
        {
            if (Status == StopwatchStatus.Running)
            {
                _timerService.Stop();
                _storedTime += _timeService.Now - _beginDateTime;
                ElapsedTime = _storedTime;
                Status = StopwatchStatus.Paused;
            }
            else
            {
                throw new InvalidOperationException($"Stopwatch status is {Status}.");
            }
        }
        /// <summary>
        /// 計測を終了し、計測結果を履歴に保存する
        /// </summary>
        public void Reset()
        {
            if (Status == StopwatchStatus.Paused)
            {
                Status = StopwatchStatus.Stoped;
                _lapTimes.Clear();
            }
            else
            {
                throw new InvalidOperationException($"Stopwatch status is {Status}.");
            }
        }
    }
}
