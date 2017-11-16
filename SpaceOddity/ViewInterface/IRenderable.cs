using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewInterface
{
    public interface IRenderable
    {
        IAction LeftClickAction { set; }
        IAction RightClickAction { set; }
        void Update(Vector2 position, Vector2 rotation, Vector2 scale);
        void Delete();
    }
}
