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
        private IFactory<IRenderable> renderableFactory;

        public ActivateableWorldObjectFactory(IFactory<IRenderable> renderableFactory)
        {
            this.renderableFactory = renderableFactory;
        }

        public IActivateableWorldObject Create()
        {
            var renderable = renderableFactory.Create();
            return new ActivateableWorldObject(
                new WorldObject(new Vector2(), new Vector2(), new Vector2(1, 1),
                    new NoAction(), new NoAction(), renderable), renderable);
        }
    }
}
