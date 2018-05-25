using Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.ModelDetailsConnection
{
    public class ViewDetailsFactory<TObject, TElement> : IFactory<TObject, TElement> where TObject : IWorldObject
    {
        private IDetails<TElement> details;
        private IFactory<TObject>[] viewFactories;

        public ViewDetailsFactory(IDetails<TElement> details, IFactory<TObject>[] viewFactories)
        {
            this.details = details;
            this.viewFactories = viewFactories;
        }

        public TObject Create(TElement element)
        {
            return viewFactories[details[element]].Create();
        }
    }
}
