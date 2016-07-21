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
        [Description("インスタンス指定で設定されたケースに正しく取得されることを確認する")]
        public void ResolveNormalTest()
        {
            var service = new ServiceA();
            ServiceLocator.Register<IServiceA>(service);

            var resolved = ServiceLocator.Resolve<IServiceA>();
            Assert.IsNotNull(resolved);
            Assert.AreEqual(service, resolved);
        }

        [TestMethod]
        public void ResolverNullTest()
        {
            ServiceLocator.Register<string>(null);
            Assert.IsNull(ServiceLocator.Resolve<string>());
        }

        public interface IServiceA
        {
        }

        public class ServiceA : IServiceA
        {
        }
    }
}
