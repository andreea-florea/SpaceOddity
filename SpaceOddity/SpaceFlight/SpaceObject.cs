using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceFlight
{
    public class SpaceObject
    {
        public Vector2 Position { get; private set; }
        public Vector2 Rotation { get; private set; }
        public Vector2 TranslationalForce { get; private set; }
        public Vector2 RotationalForce { get; private set; }

        public SpaceObject(Vector2 position, Vector2 rotation, 
            Vector2 transationalForce, Vector2 rotationalForce)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.TranslationalForce = transationalForce;
            this.RotationalForce = rotationalForce;
        }
    }
}
