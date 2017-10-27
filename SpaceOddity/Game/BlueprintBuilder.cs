using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Interfaces;
using NaturalNumbersMath;

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

        public IBlock GetBlock(Coordinate position)
        {
            return blueprint.Get(position);
        }

        public bool CreateBlock(Coordinate position)
        {
            if (GetBlock(position) == null)
            {
                blueprint[position.Y, position.X] = blockFactory.CreateBlock();
                return true;
            }
            return false;
        }

        public bool DeleteBlock(Coordinate position)
        {
            if (GetBlock(position) == null)
            {
                return false;
            }

            blueprint[position.Y, position.X] = null;
            return true;
        }

        public bool AddShipComponent(Coordinate position, IShipComponent component)
        {
            if (GetBlock(position) != null)
            {
                GetBlock(position).AddShipComponent(component);
                return true;
            }

            return false;
        }

        public bool DeleteShipComponent(Coordinate position)
        {
            if (GetBlock(position) != null && GetBlock(position).ShipComponent != null)
            {
                GetBlock(position).DeleteShipComponent();
                return true;
            }

            return false;
        }
    }
}
