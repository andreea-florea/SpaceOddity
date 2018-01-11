using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geometry
{
    public class CurveFraction : ICurve
    {
        private ICurve baseCurve;
        private double startTime;
        private double timeScale;

        public CurveFraction(ICurve baseCurve, double startTime, double timeScale)
        {
            this.baseCurve = baseCurve;
            this.startTime = startTime;
            this.timeScale = timeScale;
        }

        public Vector2 GetPoint(double t)
        {
            var t1 = startTime + t * timeScale;
            return baseCurve.GetPoint(startTime + t * timeScale);
        }
    }
}
