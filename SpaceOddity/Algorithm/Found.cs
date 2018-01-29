using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public class Found<T>
    {
        public bool IsFound { get; private set; }
        public T Element { get; private set; }

        
        public Found(bool isFound, T element)
        {
            this.IsFound = isFound;
            this.Element = element;
        }
    }
}
