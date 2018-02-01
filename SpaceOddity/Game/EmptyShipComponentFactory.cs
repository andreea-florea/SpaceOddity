using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class EmptyShipComponentFactory : IShipComponentFactory
    {
        public IShipComponent Create(IConstBlock block)
        {
            return new EmptyShipComponent();
        }
    }
}
