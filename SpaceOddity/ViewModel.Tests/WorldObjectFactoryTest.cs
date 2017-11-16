using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewInterface;
using Moq;
using Geometry;

namespace ViewModel.Tests
{
    [TestClass]
    public class WorldObjectFactoryTest
    {
        [TestMethod]
        public void WorldObjectFactoryCreatesRenderable()
        {
            var mockRenderableFactory = new Mock<IRenderableFactory>();
            var mockRenderable = new Mock<IRenderable>();
            mockRenderableFactory.Setup(factory => factory.CreateRenderable()).Returns(mockRenderable.Object);
            var worldObjectFactory = new WorldObjectFactory(mockRenderableFactory.Object);

            var worldObject = worldObjectFactory.CreateObject();
            mockRenderableFactory.Verify(factory => factory.CreateRenderable(), Times.Once());
            mockRenderable.Verify(renderable => renderable.Update(new Vector2(), new Vector2(), new Vector2(1, 1)), Times.Once());
        }

        [TestMethod]
        public void WorldObjectFactorySetsUpClicksToNoAction()
        {
            var mockRenderableFactory = new Mock<IRenderableFactory>();
            var mockRenderable = new Mock<IRenderable>();
            mockRenderableFactory.Setup(factory => factory.CreateRenderable()).Returns(mockRenderable.Object);
            var worldObjectFactory = new WorldObjectFactory(mockRenderableFactory.Object);
            
            mockRenderable.SetupSet(renderable => renderable.LeftClickAction = It.IsNotNull<NoAction>()).Verifiable();
            mockRenderable.SetupSet(renderable => renderable.RightClickAction = It.IsNotNull<NoAction>()).Verifiable();

            var worldObject = worldObjectFactory.CreateObject();

            mockRenderable.VerifySet(renderable => renderable.LeftClickAction = It.IsNotNull<NoAction>());
            mockRenderable.VerifySet(renderable => renderable.RightClickAction = It.IsNotNull<NoAction>());
        }
    }
}
