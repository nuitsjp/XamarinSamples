using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace XFStopwatch.Tests
{
    [TestClass]
    public class BindableBaseTests
    {
        [TestMethod]
        public void IsNotifiedTest()
        {
            var bindable = new TestBindable();
            bool isNotified = false;
            bindable.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Property")
                {
                    isNotified = true;
                }
            };

            bindable.Property = "Hello, World.";

            Assert.IsTrue(isNotified);
            Assert.AreEqual("Hello, World.", bindable.property);
        }

        [TestMethod]
        public void IsNotNotifiedTest()
        {
            var bindable = new TestBindable();
            bool isNotified = false;
            bindable.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Property")
                {
                    isNotified = true;
                }
            };

            bindable.Property = null;

            Assert.IsFalse(isNotified);
            Assert.IsNull(bindable.property);
        }

        public class TestBindable : BindableBase
        {
            // ReSharper disable once InconsistentNaming
            public string property;

            public string Property
            {
                get { return property; }
                set { result = SetProperty(ref property, value); }
            }

            // ReSharper disable once InconsistentNaming
            public bool result;
        }
    }
}
