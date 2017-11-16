using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel
{
    public class WorldObject : IWorldObject
    {
        private IRenderable renderable;

        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateRenderable();
            }
        }

        private Vector2 rotation;
        public Vector2 Rotation
        {
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                UpdateRenderable();
            }
        }

        private Vector2 scale;
        public Vector2 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                UpdateRenderable();
            }
        }

        public IAction LeftClickAction
        {
            set
            {
                renderable.LeftClickAction = value;
            }
        }

        public IAction RightClickAction
        {
            set
            {
                renderable.RightClickAction = value;
            }
        }

        public WorldObject(Vector2 position, Vector2 rotation, Vector2 scale,
            IAction leftClickAction, IAction rightClickAction, IRenderable renderable)
        {
            this.renderable = renderable;
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
            this.LeftClickAction = leftClickAction;
            this.RightClickAction = rightClickAction;
        }

        public void Delete()
        {
            renderable.Delete();
        }

        private void UpdateRenderable()
        {
            renderable.Update(Position, Rotation, Scale);
        }
    }
}
