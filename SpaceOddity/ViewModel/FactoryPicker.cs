using Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public class FactoryPicker<TKey, TElement> : IFactory<TElement, TKey>
    {
        private IDictionary<TKey, IFactory<TElement>> factories;

        public FactoryPicker(IDictionary<TKey, IFactory<TElement>> factories)
        {
            this.factories = factories;
        }

        public TElement Create(TKey details)
        {
            return factories[details].Create();
        }
    }
}
