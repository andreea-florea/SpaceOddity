using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewInterface;
using Moq;
using Geometry;

namespace ViewModel.Tests
{
    [TestClass]
    public class ActivateableWorldObjetFactoryTest
    {
        [TestMethod]
        public void BuilderWorldObjectFactoryCreatesRenderable()
        {
            var mockRenderableFactory = new Mock<IRenderableFactory>();
            var mockRenderable = new Mock<IRenderable>();
            mockRenderableFactory.Setup(factory => factory.CreateRenderable()).Returns(mockRenderable.Object);
            var worldObjectFactory = new ActivateableWorldObjectFactory(mockRenderableFactory.Object);

            var worldObject = worldObjectFactory.Create();
            mockRenderableFactory.Verify(factory => factory.CreateRenderable(), Times.Once());
            mockRenderable.Verify(renderable => renderable.Update(new Vector2(), new Vector2(), new Vector2(1, 1)), Times.Once());
        }

        [TestMethod]
        public void BuilderWorldObjectFactorySetsUpClicksToNoAction()
        {
            var mockRenderableFactory = new Mock<IRenderableFactory>();
            var mockRenderable = new Mock<IRenderable>();
            mockRenderableFactory.Setup(factory => factory.CreateRenderable()).Returns(mockRenderable.Object);
            var worldObjectFactory = new ActivateableWorldObjectFactory(mockRenderableFactory.Object);

            mockRenderable.SetupSet(renderable => renderable.LeftClickAction = It.IsNotNull<NoAction>()).Verifiable();
            mockRenderable.SetupSet(renderable => renderable.RightClickAction = It.IsNotNull<NoAction>()).Verifiable();

            var worldObject = worldObjectFactory.Create();

            mockRenderable.VerifySet(renderable => renderable.LeftClickAction = It.IsNotNull<NoAction>());
            mockRenderable.VerifySet(renderable => renderable.RightClickAction = It.IsNotNull<NoAction>());
        }
    }
}
