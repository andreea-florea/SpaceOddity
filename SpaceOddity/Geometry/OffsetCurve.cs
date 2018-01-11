using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry
{
    public class OffsetCurve : ICurve
    {
        private ICurve baseCurve;
        private Vector2 offset;

        public OffsetCurve(ICurve baseCurve, Vector2 offset)
        {
            this.baseCurve = baseCurve;
            this.offset = offset;
        }

        public Vector2 GetPoint(double t)
        {
            return baseCurve.GetPoint(t) + offset;
        }
    }
}
