using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry
{
    public class StraightLineCurve : ICurve
    {
        private Vector2 startPoint;
        private Vector2 vectorDirection;

        public StraightLineCurve(Vector2 startPoint, Vector2 vectorDirection)
        {
            this.startPoint = startPoint;
            this.vectorDirection = vectorDirection;
        }

        public Vector2 GetPoint(double t)
        {
            return startPoint + vectorDirection * t;
        }
    }
}
