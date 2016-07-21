using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XFStopwatch.Models;

namespace XFStopwatch.Tests.Models
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
