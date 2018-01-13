using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Geometry;
using ViewInterface;

namespace ViewModel.Tests
{
    [TestClass]
    public class ActivateableWorldObjectTest
    {
        private Mock<IWorldObject> mockBaseWorldObject;
        private Mock<IRenderable> mockRenderable;
        private ActivateableWorldObject builderWorldObject;

        [TestInitialize]
        public void Initialize()
        {
            mockBaseWorldObject = new Mock<IWorldObject>();
            mockRenderable = new Mock<IRenderable>();
            builderWorldObject = new ActivateableWorldObject(mockBaseWorldObject.Object, mockRenderable.Object);
        }

        [TestMethod]
        public void BuilderObjectUsesBaseWorldObject()
        {
            builderWorldObject.Delete();
            mockBaseWorldObject.Verify(worldObject => worldObject.Delete());

            mockBaseWorldObject.SetupSet(worldObject => worldObject.Position = new Vector2(3, 2)).Verifiable();
            builderWorldObject.Position = new Vector2(3, 2);
            mockBaseWorldObject.VerifySet(worldObject => worldObject.Position = new Vector2(3, 2), Times.Once());

            mockBaseWorldObject.SetupSet(worldObject => worldObject.Scale = new Vector2(3, 2)).Verifiable();
            builderWorldObject.Scale = new Vector2(3, 2);
            mockBaseWorldObject.VerifySet(worldObject => worldObject.Scale = new Vector2(3, 2), Times.Once());

            mockBaseWorldObject.SetupSet(worldObject => worldObject.Rotation = new Vector2(3, 2)).Verifiable();
            builderWorldObject.Rotation = new Vector2(3, 2);
            mockBaseWorldObject.VerifySet(worldObject => worldObject.Rotation = new Vector2(3, 2), Times.Once());

            var mockLeftClickAction = new Mock<IAction>();
            mockBaseWorldObject.SetupSet(worldObject => worldObject.LeftClickAction = mockLeftClickAction.Object).Verifiable();
            builderWorldObject.LeftClickAction = mockLeftClickAction.Object;
            mockBaseWorldObject.VerifySet(worldObject => worldObject.LeftClickAction = mockLeftClickAction.Object, Times.Once());

            var mockRightClickAction = new Mock<IAction>();
            mockBaseWorldObject.SetupSet(worldObject => worldObject.RightClickAction = mockRightClickAction.Object).Verifiable();
            builderWorldObject.RightClickAction = mockRightClickAction.Object;
            mockBaseWorldObject.VerifySet(worldObject => worldObject.RightClickAction = mockRightClickAction.Object, Times.Once());
        }

        [TestMethod]
        public void ChangeStateOfRenderableWhenActivated()
        {
            builderWorldObject.Activate();
            mockRenderable.Verify(renderable => renderable.SetState(1), Times.Once());
        }

        [TestMethod]
        public void ChangeStateOfRenderableWhenDeactivated()
        {
            builderWorldObject.Deactivate();
            mockRenderable.Verify(renderable => renderable.SetState(0), Times.Once());
        }
    }
}
