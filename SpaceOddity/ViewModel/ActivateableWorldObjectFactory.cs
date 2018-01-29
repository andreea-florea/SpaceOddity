using Algorithms;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel
{
    public class ActivateableWorldObjectFactory : IFactory<IActivateableWorldObject>
    {
        private IRenderableFactory renderableFactory;

        public ActivateableWorldObjectFactory(IRenderableFactory renderableFactory)
        {
            this.renderableFactory = renderableFactory;
        }

        public IActivateableWorldObject Create()
        {
            var renderable = renderableFactory.CreateRenderable();
            return new ActivateableWorldObject(
                new WorldObject(new Vector2(), new Vector2(), new Vector2(1, 1),
                    new NoAction(), new NoAction(), renderable), renderable);
        }
    }
}
