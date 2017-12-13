using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public interface IBuilderWorldObject : IWorldObject
    {
        void Deactivate();
        void Activate();
    }
}
