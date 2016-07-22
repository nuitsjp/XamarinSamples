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

            var baseDateTime = DateTime.Parse("2000/01/01");
            mockTimeService.Setup(m => m.Now).Returns(baseDateTime);

            int elapsedCount = 0;
            stopwatch.ElapsedChanged += (sender, e) => elapsedCount++;

            // 計測を開始する
            stopwatch.Start();
            Assert.AreEqual(StopwatchStatus.Running, stopwatch.Status);
            mockTimerService.Verify(m => m.Start());
            Assert.AreEqual(1, elapsedCount);
            Assert.AreEqual(baseDateTime, stopwatch.BeginDateTime);
            Assert.AreEqual(baseDateTime, stopwatch.RestartDateTime);
            Assert.AreEqual(TimeSpan.Zero, stopwatch.Elapsed);
            Assert.AreEqual(0, stopwatch.RapTimes.Count);

            // 変更を通知し時間が進むことを確認する
            var elapsedTimeSpan = TimeSpan.FromSeconds(1);
            mockTimeService.Setup(m => m.Now).Returns(baseDateTime + elapsedTimeSpan);
            mockTimerService.Raise(x => x.Elapsed += null, EventArgs.Empty);    // 時間経過を通知する

            Assert.AreEqual(2, elapsedCount);
            Assert.AreEqual(elapsedTimeSpan, stopwatch.Elapsed);
            Assert.AreEqual(0, stopwatch.RapTimes.Count);
        }

        [TestMethod]
        public void StopTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);

            var stopwatch = new Stopwatch();
            stopwatch.Stop();
            mockTimerService.Verify(m => m.Stop());
        }

        [TestMethod]
        public void RestartTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            ServiceLocator.Register(mockTimerService.Object);
            var mockTimeService = new Mock<ITimeService>();
            ServiceLocator.Register(mockTimeService.Object);

            var stopwatch = new Stopwatch();

            var baseDateTime = DateTime.Parse("2000/01/01");
            mockTimeService.Setup(m => m.Now).Returns(baseDateTime);

            // 計測を開始する
            stopwatch.Start();
            Assert.AreEqual(StopwatchStatus.Running, stopwatch.Status);
            // 1秒進める
            var elapsedTimeSpan = TimeSpan.FromSeconds(1);
            mockTimeService.Setup(m => m.Now).Returns(baseDateTime + elapsedTimeSpan);
            mockTimerService.Raise(x => x.Elapsed += null, EventArgs.Empty);

            // 一時停止する
            stopwatch.Stop();

            // 1日進める
            var restertDatetime = baseDateTime + TimeSpan.FromDays(1);
            mockTimeService.Setup(m => m.Now).Returns(restertDatetime);

            // 計測を再開する
            stopwatch.Start();

            Assert.AreEqual(baseDateTime, stopwatch.BeginDateTime);
            Assert.AreEqual(restertDatetime, stopwatch.RestartDateTime);
        }
    }
}
