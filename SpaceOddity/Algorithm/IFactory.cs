using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithm
{
    public interface IFactory<TElement>
    {
        TElement Create();
    }

    public interface IFactory<TElement, TDetails>
    {
        TElement Create(TDetails details);
    }
}
