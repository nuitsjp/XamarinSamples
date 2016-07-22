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



            var baseDateTime = DateTime.Parse("2000/01/01");

            var mockTimeService = new Mock<ITimeService>();
            mockTimeService.Setup(m => m.Now).Returns(baseDateTime);
            ServiceLocator.Register(mockTimeService.Object);

            var stopwatch = new Stopwatch();

            bool isCalledElapsedChanged = false;
            stopwatch.ElapsedChanged += (sender, e) => isCalledElapsedChanged = true;

            // 計測を開始する
            stopwatch.Start();
            mockTimerService.Verify(m => m.Start());
            Assert.IsTrue(isCalledElapsedChanged);
            Assert.AreEqual(baseDateTime, stopwatch.BeginDateTime);
            Assert.AreEqual(baseDateTime, stopwatch.RestartDateTime);
            Assert.AreEqual(TimeSpan.Zero, stopwatch.Elapsed);
            Assert.AreEqual(0, stopwatch.RapTimes.Count);

            // 変更を通知し時間が進むことを確認する
            var elapsedTimeSpan = TimeSpan.FromSeconds(1);
            isCalledElapsedChanged = false;
            mockTimeService.Setup(m => m.Now).Returns(baseDateTime + elapsedTimeSpan);
            isCalledElapsedChanged = false;
            mockTimerService.Raise(x => x.Elapsed += null, EventArgs.Empty);    // 時間経過を通知する

            Assert.IsTrue(isCalledElapsedChanged);
            Assert.AreEqual(elapsedTimeSpan, stopwatch.Elapsed);
            Assert.AreEqual(0, stopwatch.RapTimes.Count);
        }
    }
}
