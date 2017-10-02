using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Tests.Mocks
{
    public class MockBlueprintBuilderObserver : IBlueprintBuilderObserver
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public void BlockCreated(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            X = x;
            Y = y;
        }
    }
}
