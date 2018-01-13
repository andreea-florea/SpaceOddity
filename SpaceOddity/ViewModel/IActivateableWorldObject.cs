using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public interface IActivateableWorldObject : IWorldObject
    {
        void Deactivate();
        void Activate();
    }
}
