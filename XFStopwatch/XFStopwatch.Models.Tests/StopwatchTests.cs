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

        [TestMethod]
        public void StopwatchTest()
        {
            var timerServiceMock = new Mock<ITimerService>();
            ServiceLocator.Register(timerServiceMock.Object);
            var timeServiceMock = new Mock<ITimeService>();
            ServiceLocator.Register(timeServiceMock.Object);

            var actual = new Stopwatch();
            Assert.AreEqual(TimeSpan.Zero, actual.Elapsed);
            Assert.AreEqual(StopwatchStatus.Stoped, actual.Status);
            Assert.IsNotNull(actual.RapTimes);
            Assert.AreEqual(0, actual.RapTimes.Count);
        }

        [TestMethod]
        public void StartTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);

            var stopwatch = new Stopwatch();

            var beginDateTime = DateTime.Parse("2000/01/01");           // 開始日時
            mockTimeService.Setup(m => m.Now).Returns(beginDateTime);   // TimeServiceが開始日時を返却yするように設定

            int elapsedCount = 0;
            stopwatch.ElapsedChanged += (sender, e) => elapsedCount++;

            // 計測を開始する
            stopwatch.Start();
            Assert.AreEqual(StopwatchStatus.Running, stopwatch.Status); // 状態が計測中に変更されていること
            mockTimerService.Verify(m => m.Start());                    // TimerServiceのStartが呼び出されていること
            Assert.AreEqual(1, elapsedCount);                           // 時間経過通知が一度発生していること
            Assert.AreEqual(beginDateTime, stopwatch.BeginDateTime);    // 開始日時が設定されている木尾と
            Assert.AreEqual(beginDateTime, stopwatch.RestartDateTime);  // 再開日時が開始日時と同一時刻になっていること
            Assert.AreEqual(TimeSpan.Zero, stopwatch.Elapsed);          // 経過時間が0であること
            Assert.AreEqual(0, stopwatch.RapTimes.Count);               // ラップタイムの件数が0件であること

            // 変更を通知し時間が進むことを確認する
            var elapsedTimeSpan = TimeSpan.FromSeconds(1);              // 計測後の経過時間を1秒に設定する
            mockTimeService
                .Setup(m => m.Now)
                .Returns(beginDateTime + elapsedTimeSpan);              // 現在日時を開始時刻の1秒後を返却するよう設定する
            mockTimerService.Raise(x => x.Elapsed += null, EventArgs.Empty);    // 時間経過を通知する

            Assert.AreEqual(2, elapsedCount);                           // Stopwatchからの通知回数が2回になること
            Assert.AreEqual(elapsedTimeSpan, stopwatch.Elapsed);        // 経過時間が1秒になっていること
            Assert.AreEqual(0, stopwatch.RapTimes.Count);               // ラップタイムの件数が0件であうこと

            // Stopwatchを停止する
            // 停止を呼び出した場合、TimerServiceのStopが呼び出されていることを確認する
            // 正しく停止しているかどうかは、Stopwatch側ではなく、TimerService側で担保するためテストしない
            elapsedTimeSpan = TimeSpan.FromSeconds(2);                  // 計測後の経過時間を2秒に設定する
            mockTimeService
                .Setup(m => m.Now)
                .Returns(beginDateTime + elapsedTimeSpan);              // 現在日時を開始時刻の2秒後を返却するよう設定する
            stopwatch.Stop();                                           // 
            mockTimerService.Verify(m => m.Stop());

            // 1日進める
            var restertDatetime = beginDateTime + TimeSpan.FromDays(1);
            mockTimeService.Setup(m => m.Now).Returns(restertDatetime);

            // 計測を再開する
            stopwatch.Start();

            Assert.AreEqual(beginDateTime, stopwatch.BeginDateTime);
            Assert.AreEqual(restertDatetime, stopwatch.RestartDateTime);


        }

    }
}
