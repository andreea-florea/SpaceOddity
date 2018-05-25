using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.ModelDetailsConnection
{
    public interface IDetails<T>
    {
        int this[T item] { get; }
        void Add(T element, int index);
        void Remove(T element);
    }
}
