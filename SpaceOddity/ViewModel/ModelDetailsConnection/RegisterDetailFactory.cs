using Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.ModelDetailsConnection
{
    public class RegisterDetailFactory<T> : IFactory<T>
    {
        private IFactory<T> factory;
        private IDetails<T> details;
        private int index;

        public RegisterDetailFactory(IFactory<T> factory, IDetails<T> details, int index)
        {
            this.factory = factory;
            this.details = details;
            this.index = index;
        }

        public T Create()
        {
            var element = factory.Create();
            details.Add(element, index);
            return element;
        }
    }

    public class RegisterDetailFactory<T, TContext> : IFactory<T, TContext>
    {
        private IFactory<T, TContext> factory;
        private IDetails<T> details;
        private int index;

        public RegisterDetailFactory(IFactory<T, TContext> factory, IDetails<T> details, int index)
        {
            this.factory = factory;
            this.details = details;
            this.index = index;
        }

        public T Create(TContext context)
        {
            var element = factory.Create(context);
            details.Add(element, index);
            return element;
        }
    }
}
