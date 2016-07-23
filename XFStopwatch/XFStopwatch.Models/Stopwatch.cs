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
        /// 直前のラップタイム計測日時
        /// </summary>
        private DateTime _previousLapDateTime;
        /// <summary>
        /// 計測再開時刻
        /// </summary>
        /// <remarks>
        /// 開始->停止->開始とした場合に、トータルの経過時刻を計算するために最後に再開（もしくは開始）
        /// した時間を保持しておく
        /// </remarks>
        private DateTime _restertDateTime;
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
        private readonly ObservableCollection<TimeSpan> _lapTimes = new ObservableCollection<TimeSpan>();
        /// <summary>
        /// 計測結果履歴
        /// </summary>
        private readonly ObservableCollection<MeasurementResult> _measurementResult = new ObservableCollection<MeasurementResult>();

        /// <summary>
        /// 計測開始時刻を取得・設定する
        /// </summary>
        public DateTime BeginDateTime { get; private set; }
        /// <summary>
        /// 経過時間を取得・設定する
        /// </summary>
        public TimeSpan ElapsedTime
        {
            get { return _elapsedTime; }
            set
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
            set
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
        public ReadOnlyObservableCollection<TimeSpan> LapTimes { get; }
        /// <summary>
        /// 計測履歴を取得する
        /// </summary>
        public ReadOnlyObservableCollection<MeasurementResult> MeasurementResults { get; }

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
                ElapsedTime = _timeService.Now - _restertDateTime + _storedTime;
            };
            LapTimes = new ReadOnlyObservableCollection<TimeSpan>(_lapTimes);
            MeasurementResults = new ReadOnlyObservableCollection<MeasurementResult>(_measurementResult);
        }
        /// <summary>
        /// 計測を開始する
        /// </summary>
        public void Start()
        {
            if (Status == StopwatchStatus.Stoped)
            {
                BeginDateTime = _timeService.Now;
                _restertDateTime = BeginDateTime;
                _previousLapDateTime = _restertDateTime;
                Status = StopwatchStatus.Running;
                _timerService.Start();
            }
            else if(Status == StopwatchStatus.Paused)
            {
                _restertDateTime = _timeService.Now;
                _previousLapDateTime = _restertDateTime;
                Status = StopwatchStatus.Running;
                _timerService.Start();
            }
        }

        /// <summary>
        /// ラップタイムを取得する
        /// </summary>
        public void Lap()
        {
            var now = _timeService.Now;
            _lapTimes.Add(now - _previousLapDateTime);
            _previousLapDateTime = now;
        }
        /// <summary>
        /// 計測を一時停止する
        /// </summary>
        public void Pause()
        {
            _timerService.Stop();
            _storedTime += _timeService.Now - _restertDateTime;
            ElapsedTime = _storedTime;
            Status = StopwatchStatus.Paused;
        }
        /// <summary>
        /// 計測を終了し、計測結果を履歴に保存する
        /// </summary>
        public void Reset()
        {
            Status = StopwatchStatus.Stoped;
            _measurementResult.Add(
                new MeasurementResult(BeginDateTime, ElapsedTime, _lapTimes));
            _lapTimes.Clear();
        }
    }
}
