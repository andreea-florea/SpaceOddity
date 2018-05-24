using Algorithm;
using Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.ModelDetailsConnection
{
    public class ViewModelDetailsFactory<T> : IFactory<IFactory<T>>
    {
        private IFactory<T>[] factories;
        private DetailsCollection<T> details;

        public ViewModelDetailsFactory(IFactory<T>[] factories, DetailsCollection<T> details)
        {
            this.factories = factories;
            this.details = details;
        }

        public IFactory<T> Create()
        {
            var detailFactories = new IFactory<T>[factories.Length];
            for (var i = 0; i < factories.Length; ++i)
            {
                detailFactories[i] = new RegisterDetailFactory<T>(factories[i], details, i);
            }
            return new MultipleFactory<T>(factories.ToArray(), 0);
        }
    }
}
