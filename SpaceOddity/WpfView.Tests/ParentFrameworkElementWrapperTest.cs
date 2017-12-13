using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace WpfView.Tests
{
    [TestClass]
    public class ParentFrameworkElementWrapperTest
    {
        [TestMethod]
        public void ParentWrapperUsesBaseWrapper()
        {
            var stubElement = new Rectangle();
            var canvas = new Canvas();
            var mockBaseWrapper = new Mock<IFrameworkElementWrapper>();
            mockBaseWrapper.SetupGet(wrapper => wrapper.Element).Returns(stubElement);
            var parentFrameworkElementWrapper = new ParentFrameworkElementWrapper(mockBaseWrapper.Object, canvas);
            Assert.AreEqual(canvas, parentFrameworkElementWrapper.Element);
        }
    }
}
