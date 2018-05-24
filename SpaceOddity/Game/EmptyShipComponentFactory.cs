using Algorithms;
using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class EmptyShipComponentFactory : IFactory<IShipComponent, IConstBlock>
    {
        public IShipComponent Create(IConstBlock block)
        {
            return new EmptyShipComponent();
        }
    }
}
