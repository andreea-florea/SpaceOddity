using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceFlight
{
    public interface ISpaceObjectObserver
    {
        void ObjectUpdated();
    }
}
