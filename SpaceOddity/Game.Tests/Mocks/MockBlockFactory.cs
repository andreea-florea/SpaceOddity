using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Tests.Mocks
{
    public class MockBlockFactory : IBlockFactory
    {
        public MockBlock Block { get; set; }

        public MockBlockFactory(MockBlock block)
        {
            Block = block;
        }

        public IBlock CreateBlock()
        {
            return Block;
        }
    }
}
