﻿using Algorithm;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel
{
    public class CurveWorldObjectFactory : IFactory<IWorldObject, ICurve>
    {
        private ICurveRenderableFactory renderableFactory;

        public CurveWorldObjectFactory(ICurveRenderableFactory renderableFactory)
        {
            this.renderableFactory = renderableFactory;
        }

        public IWorldObject Create(ICurve details)
        {
            var renderable = renderableFactory.CreateRenderable(details);
            return new WorldObject(new Vector2(), new Vector2(), new Vector2(1, 1),
                new NoAction(), new NoAction(), renderable);
        }
    }
}