using Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    public class MultipleFactory<T> : IFactory<T>
    {
        private IFactory<T>[] factories;
        public int SelectedIndex { get; set; }

        public MultipleFactory(IFactory<T>[] factories, int selectedIndex)
        {
            this.factories = factories;
            this.SelectedIndex = selectedIndex;
        }

        public T Create()
        {
            return factories[SelectedIndex].Create();
        }
    }
}
