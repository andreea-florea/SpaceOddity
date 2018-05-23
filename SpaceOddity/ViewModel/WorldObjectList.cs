using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public class WorldObjectList<TWorldObject> : IEnumerable<TWorldObject> where TWorldObject : IWorldObject
    {
        private List<TWorldObject> objects;

        public int Count
        {
            get
            {
                return objects.Count;
            }
        }

        public TWorldObject this[int index]
        {
            get
            {
                return objects[index];
            }
        }

        public WorldObjectList()
        {
            objects = new List<TWorldObject>();
        }

        public void AddRange(IEnumerable<TWorldObject> worldObjects)
        {
            objects.AddRange(worldObjects);
        }
        
        public void Clear()
        {
            foreach (var worldObject in objects)
            {
                worldObject.Delete();
            }
            objects.Clear();
        }

        public IEnumerator<TWorldObject> GetEnumerator()
        {
            return objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return objects.GetEnumerator();
        }
    }
}
