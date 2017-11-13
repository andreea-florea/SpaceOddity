using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public static class ListExtensions
    {
        public delegate void PairUp<T>(T element1, T element2);
        static public void Pairs<T>(this List<T> list, PairUp<T> pairUp)
        {
            for (var i  = 0; i < list.Count - 1; ++i)
            {
                for (var j = i + 1; j < list.Count; ++j)
                {
                    pairUp(list[i], list[j]);
                }
            }
        }

    }
}
