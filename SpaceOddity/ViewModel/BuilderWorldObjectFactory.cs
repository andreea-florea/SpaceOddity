using Algorithm;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel
{
    public class BuilderWorldObjectFactory : IFactory<IBuilderWorldObject>
    {
        private IRenderableFactory renderableFactory;

        public BuilderWorldObjectFactory(IRenderableFactory renderableFactory)
        {
            this.renderableFactory = renderableFactory;
        }

        public IBuilderWorldObject Create()
        {
            var renderable = renderableFactory.CreateRenderable();
            return new BuilderWorldObject(
                new WorldObject(new Vector2(), new Vector2(), new Vector2(1, 1),
                    new NoAction(), new NoAction(), renderable), renderable);
        }
    }
}
