using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ViewInterface
{
    public interface IWorldObject
    {
        Vector2 Position { get; set; }
        Vector2 Scale { get; set; }
        Vector2 Rotation { get; set; }
        IAction LeftClickAction { get; set; }
        IAction RightClickAction { get; set; }
        void Delete();
    }
}
