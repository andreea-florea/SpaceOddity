using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Tests.Mocks
{
    public class MockBlock : IBlock
    {
        public double Weight { get; private set; }
    }
}
