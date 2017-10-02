using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class BlueprintBuilder : IBlueprintBuilder
    {
        private IBlock[,] blueprint;
        private IBlockFactory blockFactory;

        public int Width
        {
            get
            {
                return blueprint.GetLength(1);
            }
        }
        public int Height
        {
            get
            {
                return blueprint.GetLength(0);
            }
        }

        public BlueprintBuilder(IBlock[,] blueprint, IBlockFactory blockFactory)
        {
            this.blueprint = blueprint;
            this.blockFactory = blockFactory;
        }


        public IBlock GetBlock(int y, int x)
        {
            return blueprint[y, x];
        }

        public bool CreateBlock(int y, int x)
        {
            if (blueprint[y, x] == null)
            {
                blueprint[y, x] = blockFactory.CreateBlock();
                return true;
            }
            return false;
        }
    }
}
