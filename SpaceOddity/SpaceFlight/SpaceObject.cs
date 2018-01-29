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
        private Vector2 position;
        public Vector2 Position 
        { 
            get
            {
                return position;
            }
            set
            {
                position = value;
                UpdateObservers();
            }
        }

        private Vector2 rotation;
        public Vector2 Rotation 
        { 
            get
            {
                return rotation;
            }
            set
            {
                rotation = value;
                UpdateObservers();
            }
        }

        private IList<ISpaceObjectObserver> observers;

        public SpaceObject(Vector2 position, Vector2 rotation)
        {
            observers = new List<ISpaceObjectObserver>();
            this.Position = position;
            this.Rotation = rotation;
        }

        private void UpdateObservers()
        {
            foreach (var observer in observers)
            {
                observer.ObjectUpdated();
            }
        }

        public void AttachObserver(ISpaceObjectObserver observer)
        {
            observers.Add(observer);
        }
    }
}
