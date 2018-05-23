using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Controls;
using Moq;
using Geometry;
using Algorithms;

namespace WpfView.Tests
{
    [TestClass]
    public class WpfCurveRenderableFactoryTest
    {
        private Canvas canvas;
        private Mock<ICurve> mockCurve;
        private Mock<IFactory<IFrameworkElementWrapper, ICurve>> mockElementFactory;
        private BuilderWorldObjectState[] states;
        private WpfCurveRenderableFactory wpfRenderableFactory;

        [TestInitialize]
        public void Initialize()
        {
            canvas = new Canvas();
            mockCurve = new Mock<ICurve>();
            mockElementFactory = new Mock<IFactory<IFrameworkElementWrapper, ICurve>>();
            states = new BuilderWorldObjectState[1];

            wpfRenderableFactory = new WpfCurveRenderableFactory(canvas, states, 2, 8);
        }

        [TestMethod]
        public void CurveRenderableIsCreatedAddedToCanvas()
        {
            //wpfRenderableFactory.Create(mockCurve.Object);
        }
    }
}
