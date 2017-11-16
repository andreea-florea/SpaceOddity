using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewInterface
{
    public interface IRenderableFactory
    {
        IRenderable CreateRenderable();
    }
}
