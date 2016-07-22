using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XFStopwatch.Models.Tests
{
    [TestClass]
    public class TimeServiceTest
    {
        [TestMethod]
        public void NowTest()
        {
            var timeService = new TimeService();
            var before = DateTime.Now;
            // 時間をずらすため少し止める
            Thread.Sleep(10);
            var now = timeService.Now;
            Assert.IsTrue(before < now);
            // 時間をずらすため少し止める
            Thread.Sleep(10);
            Assert.IsTrue(now < DateTime.Now);
        }
    }
}
