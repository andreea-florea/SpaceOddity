using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms
{
    public static class ListExtensions
    {
        public static void ForeachPair<T>(this IEnumerable<T> enumerable, Action<T, T> pairFunction)
        {
            var list = enumerable.ToList();
            for (var i  = 0; i < list.Count - 1; ++i)
            {
                for (var j = i + 1; j < list.Count; ++j)
                {
                    pairFunction(list[i], list[j]);
                }
            }
        }

    }
}
