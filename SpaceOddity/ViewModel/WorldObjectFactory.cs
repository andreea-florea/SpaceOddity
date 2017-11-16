using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel
{
    public class WorldObjectFactory : IWorldObjectFactory
    {
        private IRenderableFactory renderableFactory;

        public WorldObjectFactory(IRenderableFactory renderableFactory)
        {
            this.renderableFactory = renderableFactory;
        }

        public IWorldObject CreateObject()
        {
            var renderable = renderableFactory.CreateRenderable();
            return new WorldObject(new Vector2(), new Vector2(), new Vector2(1, 1),
                new NoAction(), new NoAction(), renderable);
        }
    }
}
