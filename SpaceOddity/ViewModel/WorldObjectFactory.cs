using Algorithms;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel
{
    public class WorldObjectFactory : IFactory<IWorldObject>
    {
        private IFactory<IRenderable> renderableFactory;

        public WorldObjectFactory(IFactory<IRenderable> renderableFactory)
        {
            this.renderableFactory = renderableFactory;
        }

        public IWorldObject Create()
        {
            var renderable = renderableFactory.Create();
            return new WorldObject(new Vector2(), new Vector2(), new Vector2(1, 1),
                new NoAction(), new NoAction(), renderable);
        }
    }
}
