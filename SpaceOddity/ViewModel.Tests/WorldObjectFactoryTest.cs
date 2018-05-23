using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ViewInterface;
using Moq;
using Geometry;
using Algorithms;

namespace ViewModel.Tests
{
    [TestClass]
    public class WorldObjectFactoryTest
    {
        private Mock<IFactory<IRenderable>> mockRenderableFactory;
        private Mock<IRenderable> mockRenderable;
        private WorldObjectFactory worldObjectFactory;

        [TestInitialize]
        public void Initialize()
        {
            mockRenderableFactory = new Mock<IFactory<IRenderable>>();
            mockRenderable = new Mock<IRenderable>();
            mockRenderableFactory.Setup(factory => factory.Create()).Returns(mockRenderable.Object);
            worldObjectFactory = new WorldObjectFactory(mockRenderableFactory.Object);
        }

        [TestMethod]
        public void WorldObjectFactoryCreatesRenderable()
        {
            var worldObject = worldObjectFactory.Create();
            mockRenderableFactory.Verify(factory => factory.Create(), Times.Once());
            mockRenderable.Verify(renderable => renderable.Update(new Vector2(), new Vector2(), new Vector2(1, 1)), Times.Once());
        }

        [TestMethod]
        public void WorldObjectFactorySetsUpClicksToNoAction()
        {
            mockRenderable.SetupSet(renderable => renderable.LeftClickAction = It.IsNotNull<NoAction>()).Verifiable();
            mockRenderable.SetupSet(renderable => renderable.RightClickAction = It.IsNotNull<NoAction>()).Verifiable();

            var worldObject = worldObjectFactory.Create();

            mockRenderable.VerifySet(renderable => renderable.LeftClickAction = It.IsNotNull<NoAction>());
            mockRenderable.VerifySet(renderable => renderable.RightClickAction = It.IsNotNull<NoAction>());
        }
    }
}
