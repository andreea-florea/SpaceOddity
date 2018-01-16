using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    public interface IConstDictionary<TKey, TElement>
    {
        void Add(TKey key, TElement element);
        void Remove(TKey key);
    }
}
