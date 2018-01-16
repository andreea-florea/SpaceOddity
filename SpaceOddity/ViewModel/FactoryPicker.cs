using Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public class FactoryPicker<TKey, TElement> : IFactory<TElement, TKey>, IConstDictionary<TKey, IFactory<TElement>>
    {
        private IDictionary<TKey, IFactory<TElement>> factories;

        public FactoryPicker(IDictionary<TKey, IFactory<TElement>> factories)
        {
            this.factories = factories;
        }

        public void Add(TKey key, IFactory<TElement> factory)
        {
            factories.Add(key, factory);
        }

        public void Remove(TKey key)
        {
            factories.Remove(key);
        }

        public TElement Create(TKey details)
        {
            return factories[details].Create();
        }
    }
}
