using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewInterface
{
    public interface IWorldObject
    {
        Vector2 Position { get; set; }
        Vector2 Scale { get; set; }
        IAction LeftClickAction { get; set; }
    }
}
