using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace XFStopwatch.Models.Tests
{
    [TestClass]
    public class StopwatchTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            ServiceLocator.Clear();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            ServiceLocator.Clear();
        }

        /// <summary>
        /// コンストラクタ実行直後の状態確認
        /// </summary>
        [TestMethod]
        public void StopwatchTest()
        {
            var timerServiceMock = new Mock<ITimerService>();
            ServiceLocator.Register(timerServiceMock.Object);
            var timeServiceMock = new Mock<ITimeService>();
            ServiceLocator.Register(timeServiceMock.Object);

            var actual = new Stopwatch();
            Assert.AreEqual(TimeSpan.Zero, actual.ElapsedTime);
            Assert.AreEqual(StopwatchStatus.Stoped, actual.Status);
            Assert.IsNotNull(actual.LapTimes);
            Assert.AreEqual(0, actual.LapTimes.Count);
        }

        /// <summary>
        /// シナリオに沿ったテスト：01
        /// </summary>
        /// <remarks>
        /// 以下のシナリオに従ったテストを実施する
        /// 1. Start
        /// 2. Pause
        /// 3. Start    再開後、停止期間中の時間経過を考慮した再計測が行われることを確認する
        /// 4. Pause
        /// 5. Reset
        /// </remarks>
        [TestMethod]
        public void ScenarioTest01()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);

            IStopwatch stopwatch = new Stopwatch();

            var beginDateTime = DateTime.Parse("2000/01/01");           // 開始日時
            mockTimeService.Setup(m => m.Now).Returns(beginDateTime);   // TimeServiceが開始日時を返却yするように設定

            int notifiedElapsedTimeChangedCount = 0;
            stopwatch.ElapsedTimeChanged += (sender, args) => notifiedElapsedTimeChangedCount++;
            int notifiedStatusChangedCount = 0;
            stopwatch.StatusChanged += (sender, args) => notifiedStatusChangedCount++;

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 1. Start
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            stopwatch.Start();
            Assert.AreEqual(0, notifiedElapsedTimeChangedCount);        // 時間経過通知が発生していないこと
            Assert.AreEqual(1, notifiedStatusChangedCount);             // ステータス変更通知が発生していること
            Assert.AreEqual(StopwatchStatus.Running, stopwatch.Status); // 状態が計測中に変更されていること
            Assert.AreEqual(beginDateTime, stopwatch.BeginDateTime);    // 開始日時が設定されていること
            Assert.AreEqual(TimeSpan.Zero, stopwatch.ElapsedTime);      // 経過時間が0であること
            Assert.AreEqual(0, stopwatch.LapTimes.Count);               // ラップタイムの件数が0件であること
            mockTimerService.Verify(m => m.Start());                    // TimerServiceのStartが呼び出されていること

            // TimerServiceから時間経過を通知し、時間が進むことを確認する
            var elapsedTimeSpan = TimeSpan.FromSeconds(1);              // 計測後の経過時間を1秒に設定する
            mockTimeService
                .Setup(m => m.Now)
                .Returns(beginDateTime + elapsedTimeSpan);              // 現在日時を開始時刻の1秒後を返却するよう設定する
            mockTimerService.Raise(x => x.Elapsed += null, EventArgs.Empty);// 時間経過を通知する

            Assert.AreEqual(1, notifiedElapsedTimeChangedCount);        // Stopwatchからの通知回数が1回になること
            Assert.AreEqual(elapsedTimeSpan, stopwatch.ElapsedTime);    // 経過時間が1秒になっていること
            Assert.AreEqual(0, stopwatch.LapTimes.Count);               // ラップタイムの件数が0件であうこと


            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 2. Pause
            // 停止を呼び出した場合、TimerServiceのStopが呼び出されていることを確認する
            // 正しく停止しているかどうかは、Stopwatch側ではなく、TimerService側で担保するためテストしない
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            elapsedTimeSpan = TimeSpan.FromSeconds(2);                  // 計測後の経過時間を2秒に設定する
            mockTimeService
                .Setup(m => m.Now)
                .Returns(beginDateTime + elapsedTimeSpan);              // 現在日時を開始時刻の2秒後を返却するよう設定する

            stopwatch.Pause();

            Assert.AreEqual(2, notifiedElapsedTimeChangedCount);        // 時間経過通知が発生していないこと
            Assert.AreEqual(2, notifiedStatusChangedCount);             // ステータス変更通知が発生していること
            Assert.AreEqual(StopwatchStatus.Paused, stopwatch.Status);  // 状態が計測中に変更されていること
            Assert.AreEqual(beginDateTime, stopwatch.BeginDateTime);    // 開始日時が変更されていないこと
            Assert.AreEqual(elapsedTimeSpan, stopwatch.ElapsedTime);    // 経過時間が2秒であること
            Assert.AreEqual(0, stopwatch.LapTimes.Count);               // ラップタイムの件数が0件であること
            mockTimerService.Verify(m => m.Stop());                     // TimeServiceの停止が呼び出されていること


            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 3. Start    再開後、停止期間中の時間経過を考慮した再計測が行われることを確認する
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 1日進める
            var restertDatetime = beginDateTime + elapsedTimeSpan + TimeSpan.FromDays(1);
            mockTimeService.Setup(m => m.Now).Returns(restertDatetime);

            // 計測を再開する
            stopwatch.Start();

            Assert.AreEqual(2, notifiedElapsedTimeChangedCount);        // 時間経過通知が発生していないこと
            Assert.AreEqual(3, notifiedStatusChangedCount);             // ステータス変更通知が発生していること
            Assert.AreEqual(StopwatchStatus.Running, stopwatch.Status); // 状態が計測中に変更されていること
            Assert.AreEqual(beginDateTime, stopwatch.BeginDateTime);    // 開始日時が変更されていないこと
            Assert.AreEqual(elapsedTimeSpan, stopwatch.ElapsedTime);    // 経過時間が2秒であること
            Assert.AreEqual(0, stopwatch.LapTimes.Count);               // ラップタイムの件数が0件であること
            mockTimerService.Verify(m => m.Start());                    // TimerServiceのStartが呼び出されていること

            // TimerServiceから時間経過を通知し、時間が進むことを確認する
            elapsedTimeSpan = TimeSpan.FromSeconds(1);                  // 計測後の経過時間を1秒に設定する
            mockTimeService
                .Setup(m => m.Now)
                .Returns(restertDatetime + elapsedTimeSpan);            // 現在日時を再開時刻の1秒後を返却するよう設定する
            mockTimerService.Raise(x => x.Elapsed += null, EventArgs.Empty);// 時間経過を通知する

            Assert.AreEqual(3, notifiedElapsedTimeChangedCount);        // Stopwatchからの通知回数が3回になること
            Assert.AreEqual(
                TimeSpan.FromSeconds(3), stopwatch.ElapsedTime);        // 経過時間が3秒になっていること
            Assert.AreEqual(0, stopwatch.LapTimes.Count);               // ラップタイムの件数が0件であうこと


            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 4. Pause
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            elapsedTimeSpan = TimeSpan.FromSeconds(2);                  // 再開後の経過時間を2秒に設定する
            mockTimeService
                .Setup(m => m.Now)
                .Returns(restertDatetime + elapsedTimeSpan);            // 現在日時を開始時刻の2秒後を返却するよう設定する

            stopwatch.Pause();

            Assert.AreEqual(4, notifiedElapsedTimeChangedCount);        // 時間経過通知が発生していないこと
            Assert.AreEqual(4, notifiedStatusChangedCount);             // ステータス変更通知が発生していること
            Assert.AreEqual(StopwatchStatus.Paused, stopwatch.Status);  // 状態が計測中に変更されていること
            Assert.AreEqual(beginDateTime, stopwatch.BeginDateTime);    // 開始日時が変更されていないこと
            Assert.AreEqual(
                TimeSpan.FromSeconds(4), stopwatch.ElapsedTime);        // 経過時間が4秒であること
            Assert.AreEqual(0, stopwatch.LapTimes.Count);               // ラップタイムの件数が0件であること
            mockTimerService.Verify(m => m.Stop());                     // TimeServiceの停止が呼び出されていること

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 5. Reset
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            stopwatch.Reset();
            Assert.AreEqual(4, notifiedElapsedTimeChangedCount);        // 時間経過通知が発生していないこと
            Assert.AreEqual(5, notifiedStatusChangedCount);             // ステータス変更通知が発生していること
            Assert.AreEqual(StopwatchStatus.Stoped, stopwatch.Status);  // 状態が計測中に変更されていること
            Assert.AreEqual(beginDateTime, stopwatch.BeginDateTime);    // 開始日時が変更されていないこと
            Assert.AreEqual(
                TimeSpan.FromSeconds(4), stopwatch.ElapsedTime);        // 経過時間が4秒であること
            Assert.AreEqual(0, stopwatch.LapTimes.Count);               // ラップタイムの件数が0件であること
        }

        /// <summary>
        /// シナリオに沿ったテスト：02
        /// </summary>
        /// <remarks>
        /// 以下のシナリオに従ったテストを実施する
        /// 1. Start
        /// 2. Lap
        /// 3. Lap
        /// 4. Pause
        /// 5. Start
        /// 6. Lap
        /// 7. Pause
        /// 8. Reset
        /// </remarks>
        [TestMethod]
        public void ScenarioTest02()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);

            IStopwatch stopwatch = new Stopwatch();

            var now = DateTime.Parse("2000/01/01");                         // 開始日時
            mockTimeService.Setup(m => m.Now).Returns(now);                 // TimeServiceが開始日時を返却yするように設定

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 1. Start
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            stopwatch.Start();
            Assert.AreEqual(0, stopwatch.LapTimes.Count);                   // ラップタイムの件数が0件であること

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 2. Lap
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            now += TimeSpan.FromSeconds(1);                                 // 1秒経過させる
            mockTimeService.Setup(m => m.Now).Returns(now);
            stopwatch.Lap();
            Assert.AreEqual(1, stopwatch.LapTimes.Count);                   // ラップタイムの件数が1件であること
            Assert.AreEqual(TimeSpan.FromSeconds(1), stopwatch.LapTimes[0]);// 1件目のラップタイムが1秒であること
            Assert.AreEqual(StopwatchStatus.Running, stopwatch.Status);     // 状態が計測中のままであること

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 3. Lap
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            now += TimeSpan.FromSeconds(2);                                 // 2秒経過させる
            mockTimeService.Setup(m => m.Now).Returns(now);
            stopwatch.Lap();
            Assert.AreEqual(2, stopwatch.LapTimes.Count);                   // ラップタイムの件数が2件であること
            Assert.AreEqual(TimeSpan.FromSeconds(1), stopwatch.LapTimes[0]);// 1件目のラップタイムが1秒であること
            Assert.AreEqual(TimeSpan.FromSeconds(2), stopwatch.LapTimes[1]);// 2件目のラップタイムが2秒であること
            Assert.AreEqual(StopwatchStatus.Running, stopwatch.Status);     // 状態が計測中のままであること

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 4. Pause
            // 5. Start
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            stopwatch.Pause();

            now += TimeSpan.FromDays(1);                                 // 1日経過させる
            mockTimeService.Setup(m => m.Now).Returns(now);

            stopwatch.Start();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 6. Lap
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            now += TimeSpan.FromSeconds(3);                                 // 3秒経過させる
            mockTimeService.Setup(m => m.Now).Returns(now);
            stopwatch.Lap();
            Assert.AreEqual(3, stopwatch.LapTimes.Count);                   // ラップタイムの件数が3件であること
            Assert.AreEqual(TimeSpan.FromSeconds(1), stopwatch.LapTimes[0]);// 1件目のラップタイムが1秒であること
            Assert.AreEqual(TimeSpan.FromSeconds(2), stopwatch.LapTimes[1]);// 2件目のラップタイムが2秒であること
            Assert.AreEqual(TimeSpan.FromSeconds(3), stopwatch.LapTimes[2]);// 3件目のラップタイムが3秒であること
            Assert.AreEqual(StopwatchStatus.Running, stopwatch.Status);     // 状態が計測中のままであること

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // 7. Pause
            // 8. Reset
            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            stopwatch.Pause();
            stopwatch.Reset();
            Assert.AreEqual(0, stopwatch.LapTimes.Count);                   // ラップタイムの件数が0件であること
            Assert.AreEqual(1, stopwatch.MeasurementResults.Count);         // 計測結果履歴が1件になっていること
            Assert.AreEqual(
                DateTime.Parse("2000/01/01"), 
                stopwatch.MeasurementResults[0].BeginDateTime);             // 開始日時がStart日時と一致すること
            Assert.AreEqual(TimeSpan.FromSeconds(6), stopwatch.MeasurementResults[0].ElapsedTime);// 経過日時が6秒（ラップの総和）であること
            Assert.AreEqual(TimeSpan.FromSeconds(1), stopwatch.MeasurementResults[0].LapTimes[0]);// 1件目のラップタイムが1秒であること
            Assert.AreEqual(TimeSpan.FromSeconds(2), stopwatch.MeasurementResults[0].LapTimes[1]);// 2件目のラップタイムが2秒であること
            Assert.AreEqual(TimeSpan.FromSeconds(3), stopwatch.MeasurementResults[0].LapTimes[2]);// 3件目のラップタイムが3秒であること
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void StartInTheRunningTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);
            IStopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            stopwatch.Start();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LapInTheStopedTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);
            IStopwatch stopwatch = new Stopwatch();

            stopwatch.Lap();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LapInThePausedTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);
            IStopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            stopwatch.Pause();
            stopwatch.Lap();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PauseInTheStopedTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);
            IStopwatch stopwatch = new Stopwatch();

            stopwatch.Pause();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PauseInThePauseTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);
            IStopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            stopwatch.Pause();
            stopwatch.Pause();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ResetInTheStopedTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);
            IStopwatch stopwatch = new Stopwatch();

            stopwatch.Reset();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ResetInTheRunningTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);
            IStopwatch stopwatch = new Stopwatch();

            stopwatch.Start();
            stopwatch.Reset();
        }
    }
}
