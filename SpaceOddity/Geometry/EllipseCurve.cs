using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry
{
    public class EllipseCurve : ICurve
    {
        private Vector2 radiuses;

        public EllipseCurve(Vector2 radiuses)
        {
            this.radiuses = radiuses;
        }

        public Vector2 GetPoint(double t)
        {
            return new Vector2(radiuses.X * Math.Cos(t), radiuses.Y * Math.Sin(t));
        }
    }
}
