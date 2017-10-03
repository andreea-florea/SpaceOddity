using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Tests.Mocks
{
    public class MockBlueprintBuilder : IBlueprintBuilder
    {
        public int Height { get; private set; }

        public int Width { get; private set; }

        public bool CanCreateBlock { get; set; }

        public bool CanDeleteBlock { get; set; }

        public MockBlock Block { get; private set; }

        public int GetBlockY { get; private set; }
        public int GetBlockX { get; private set; }

        public MockBlueprintBuilder(int height, int width)
        {
            this.Height = height;
            this.Width = width;
            Block = new MockBlock();
        }

        public bool CreateBlock(int y, int x)
        {
            return CanCreateBlock;
        }

        public bool DeleteBlock(int y, int x)
        {
            return CanDeleteBlock;
        }

        public IBlock GetBlock(int y, int x)
        {
            GetBlockY = y;
            GetBlockX = x;
            return Block;
        } 
    }
}
