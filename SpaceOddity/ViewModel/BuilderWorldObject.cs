using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel
{
    public class BuilderWorldObject : IBuilderWorldObject
    {
        private enum ActiveState
        {
            Normal,
            Activated
        }

        private IWorldObject baseObject;
        private IRenderable renderable;

        public Vector2 Position
        {
            get
            {
                return baseObject.Position;
            }
            set
            {
                baseObject.Position = value;
            }
        }

        public Vector2 Rotation
        {
            get
            {
                return baseObject.Rotation;
            }
            set
            {
                baseObject.Rotation = value;
            }
        }

        public Vector2 Scale
        {
            get
            {
                return baseObject.Scale;
            }
            set
            {
                baseObject.Scale = value;
            }
        }

        public IAction LeftClickAction
        {
            set 
            {
                baseObject.LeftClickAction = value;
            }
        }

        public IAction RightClickAction
        {
            set 
            {
                baseObject.RightClickAction = value;
            }
        }

        public BuilderWorldObject(IWorldObject baseObject, IRenderable renderable)
        {
            this.baseObject = baseObject;
            this.renderable = renderable;
        }

        public void Delete()
        {
            baseObject.Delete();
        }

        public void Deactivate()
        {
            renderable.SetState((int)ActiveState.Normal);
        }

        public void Activate()
        {
            renderable.SetState((int)ActiveState.Activated);
        }
    }
}
