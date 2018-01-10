using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    public struct InterchangeablePair<T>
    {
        public T First { get; private set; }
        public T Second { get; private set; }

        public InterchangeablePair(T first, T second) : this()
        {
            this.First = first;
            this.Second = second;
        }

        public static bool operator ==(InterchangeablePair<T> a, InterchangeablePair<T> b)
        {
            return a.First.Equals(b.First) && a.Second.Equals(b.Second)
                || a.First.Equals(b.Second) && a.Second.Equals(b.First);
        }

        public static bool operator !=(InterchangeablePair<T> a, InterchangeablePair<T> b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            var other = (InterchangeablePair<T>)obj;
            return this == other;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
