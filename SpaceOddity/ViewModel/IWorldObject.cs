using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;


namespace ViewModel
{
    public interface IWorldObject
    {
        Vector2 Position { get; set; }
        Vector2 Rotation { get; set; }
        Vector2 Scale { get; set; }
        IAction LeftClickAction { set; }
        IAction RightClickAction { set; }
        void Delete();
    }
}
