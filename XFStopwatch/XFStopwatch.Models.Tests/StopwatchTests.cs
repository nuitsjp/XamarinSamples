using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Smtp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using XFStopwatch.Models;

namespace XFStopwatch.Tests.Models
{
    [TestClass]
    public class StopwatchTests
    {
        [TestMethod]
        public void StartTest()
        {
            var mockTimerService = new Mock<ITimerService>();
            bool isCalledStart = false;
            mockTimerService.Setup(m => m.Start()).Callback(() => isCalledStart = true);



            var mockTimeService = new Mock<ITimeService>();

            var baseDateTime = DateTime.Parse("2000/01/01");
            mockTimeService.Setup(m => m.Now).Returns(baseDateTime);

            var stopwatch = new Stopwatch(mockTimeService.Object, mockTimerService.Object);

            bool isCalledElapsedChanged = false;
            stopwatch.ElapsedChanged += (sender, e) => isCalledElapsedChanged = true;

            // 計測を開始する
            stopwatch.Start();
            Assert.IsTrue(isCalledStart);
            Assert.IsTrue(isCalledElapsedChanged);
            Assert.AreEqual(TimeSpan.Zero, stopwatch.Elapsed);
            Assert.AreEqual(0, stopwatch.RapTimes.Count);

            // 変更を通知し時間が進むことを確認する
            var elapsedTimeSpan = TimeSpan.FromSeconds(1);
            mockTimeService.Setup(m => m.Now).Returns(baseDateTime + elapsedTimeSpan);
            isCalledElapsedChanged = false;
            mockTimerService.Raise(x => x.Elapsed += null, EventArgs.Empty);    // 時間経過を通知する

            Assert.IsTrue(isCalledElapsedChanged);
            Assert.AreEqual(elapsedTimeSpan, stopwatch.Elapsed);
            Assert.AreEqual(0, stopwatch.RapTimes.Count);
        }
    }
}
