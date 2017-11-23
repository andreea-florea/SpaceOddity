using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry
{
    public interface ICurve
    {
        Vector2 GetPoint(double t);
    }
}
