using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.ModelDetailsConnection
{
    public class Details<T> : IDetails<T>
    {
        private Dictionary<T, int> factoryIndex;

        public int this[T item]
        {
            get
            {
                return factoryIndex[item];
            }
        }

        public Details()
        {
            factoryIndex = new Dictionary<T, int>();
        }

        public void Add(T element, int index)
        {
            factoryIndex.Add(element, index);
        }

        public void Remove(T element)
        {
            factoryIndex.Remove(element);
        }
    }
}
