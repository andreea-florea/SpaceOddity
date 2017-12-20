using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewInterface;
using Geometry;

namespace ViewModel.Tests
{
    [TestClass]
    public class CurveWorldObjectFactoryTest
    {
        private Mock<ICurveRenderableFactory> mockRenderableFactory;
        private Mock<IRenderable> mockRenderable;
        private CurveWorldObjectFactory worldObjectFactory;
        private Mock<ICurve> mockCurve;

        [TestInitialize]
        public void Initialize()
        {
            mockRenderableFactory = new Mock<ICurveRenderableFactory>();
            mockRenderable = new Mock<IRenderable>();
            mockCurve = new Mock<ICurve>();
            mockRenderableFactory.Setup(factory => factory.CreateRenderable(mockCurve.Object)).Returns(mockRenderable.Object);
            worldObjectFactory = new CurveWorldObjectFactory(mockRenderableFactory.Object);
        }

        [TestMethod]
        public void CurveWorldObjectFactoryCreatesRenderable()
        {
            var worldObject = worldObjectFactory.Create(mockCurve.Object);
            mockRenderableFactory.Verify(factory => factory.CreateRenderable(mockCurve.Object), Times.Once());
            mockRenderable.Verify(renderable => renderable.Update(new Vector2(), new Vector2(), new Vector2(1, 1)), Times.Once());
        }

        [TestMethod]
        public void CurveWorldObjectFactorySetsUpClicksToNoAction()
        {
            mockRenderable.SetupSet(renderable => renderable.LeftClickAction = It.IsNotNull<NoAction>()).Verifiable();
            mockRenderable.SetupSet(renderable => renderable.RightClickAction = It.IsNotNull<NoAction>()).Verifiable();

            var worldObject = worldObjectFactory.Create(mockCurve.Object);

            mockRenderable.VerifySet(renderable => renderable.LeftClickAction = It.IsNotNull<NoAction>());
            mockRenderable.VerifySet(renderable => renderable.RightClickAction = It.IsNotNull<NoAction>());
        }
    }
}
