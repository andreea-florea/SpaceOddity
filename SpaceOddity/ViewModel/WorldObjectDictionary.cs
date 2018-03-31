using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel;

namespace ViewModel
{
    public class WorldObjectDictionary<TKey, TValue> where TValue : IWorldObject
    {
        private Dictionary<TKey, TValue> collection;

        public WorldObjectDictionary()
        {
            collection = new Dictionary<TKey, TValue>();
        }

        public TValue this[TKey key]
        {
            get
            {
                return collection[key];
            }
            set
            {
                collection[key] = value;
            }
        }

        public void Add(TKey key, TValue value)
        {
            collection.Add(key, value);
        }

        public void Remove(TKey key)
        {
            collection[key].Delete();
            collection.Remove(key);
        }

        public bool ContainsKey(TKey key)
        {
            return collection.ContainsKey(key);
        }
    }
}
