using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XFStopwatch.Tests
{
    [TestClass]
    public class ServiceLocatorTests
    {
        [TestMethod]
        public void TestLocateNormal()
        {
            var sercice = new Service();
            ServiceLocator.Register<IService>(sercice);

            var actual = ServiceLocator.Locate<IService>();
            Assert.IsNotNull(actual);
            Assert.AreEqual(sercice, actual);
        }

        [TestMethod]
        public void TestLocateNullValue()
        {
            ServiceLocator.Register<IService>(null);
            Assert.IsNull(ServiceLocator.Locate<IService>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestLocateNotRegistered()
        {
            ServiceLocator.Locate<string>();
        }

        public interface IService
        {
        }

        public class Service : IService
        {
        }
    }
}
