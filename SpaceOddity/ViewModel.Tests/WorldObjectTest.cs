using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ViewInterface;
using Geometry;

namespace ViewModel.Tests
{
    [TestClass]
    public class WorldObjectTest
    {
        [TestMethod]
        public void WorldObjectUpdatesRenderableWithCorrectTransform()
        {
            var mockRenderable = new Mock<IRenderable>();
            var worldObject = new WorldObject(new Vector2(), new Vector2(), new Vector2(),
                null, null, mockRenderable.Object);

            var position = new Vector2(1, 2);
            var rotation = new Vector2(3, 4);
            var scale = new Vector2(6, 5);

            worldObject.Position = position;
            mockRenderable.Verify(renderable => renderable.Update(position, new Vector2(), new Vector2()), Times.Once());
            worldObject.Rotation = rotation;
            mockRenderable.Verify(renderable => renderable.Update(position, rotation, new Vector2()), Times.Once());
            worldObject.Scale = scale;
            mockRenderable.Verify(renderable => renderable.Update(position, rotation, scale), Times.Once());
        }

        [TestMethod]
        public void DeleteRenderableWhenWorldObjectIsDeleted()
        {
            var mockRenderable = new Mock<IRenderable>();
            var worldObject = new WorldObject(new Vector2(), new Vector2(), new Vector2(),
                null, null, mockRenderable.Object);

            worldObject.Delete();

            mockRenderable.Verify(renderable => renderable.Delete());
        }

        [TestMethod]
        public void WorldObjectAssignesActionsToRenderable()
        {
            var mockRenderable = new Mock<IRenderable>();
            mockRenderable.SetupAllProperties();
            var mockLeftClickAction = new Mock<IAction>();
            var mockRightClickAction = new Mock<IAction>();
            var worldObject = new WorldObject(new Vector2(), new Vector2(), new Vector2(),
                null, null, mockRenderable.Object);

            worldObject.LeftClickAction = mockLeftClickAction.Object;
            worldObject.RightClickAction = mockRightClickAction.Object;

            mockRenderable.VerifySet(renderable => renderable.LeftClickAction = mockLeftClickAction.Object, Times.Once());
            mockRenderable.VerifySet(renderable => renderable.RightClickAction = mockRightClickAction.Object, Times.Once());
        }
    }
}
