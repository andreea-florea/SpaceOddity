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
        private IShipComponentFactory shipComponentFactory;

        public Coordinate Dimensions
        {
            get 
            {
                return new Coordinate(blueprint.GetLength(1), blueprint.GetLength(0));
            }
        }

        public BlueprintBuilder(IBlock[,] blueprint, IBlockFactory blockFactory, IShipComponentFactory shipComponentFactory)
        {
            this.blueprint = blueprint;
            this.blockFactory = blockFactory;
            this.shipComponentFactory = shipComponentFactory;
        }

        public BlueprintBuilder(Coordinate dimensions)
        {
            //TODO: check negatives?
            blueprint = new IBlock[dimensions.X, dimensions.Y];
            blockFactory = new BlockFactory(5);
            shipComponentFactory = new BatteryFactory();
        }

        public IBlock GetBlock(Coordinate position)
        {
            return blueprint.Get(position);
        }

        public bool HasBlock(Coordinate position)
        {
            return (blueprint.IsWithinBounds(position) && GetBlock(position) != null);
        }

        public bool CreateBlock(Coordinate position)
        {
            //TODO: check that position in within bounds?
            if (GetBlock(position) == null)
            {
                blueprint.Set(position, blockFactory.CreateBlock());
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

            blueprint.Set(position, null);
            return true;
        }

        public bool AddShipComponent(Coordinate position)
        {
            var component = shipComponentFactory.CreateComponent();
            if (GetBlock(position) != null && !(GetBlock(position).HasShipComponent()))
            {
                GetBlock(position).AddShipComponent(component);
                return true;
            }

            return false;
        }

        public bool DeleteShipComponent(Coordinate position)
        {
            if (GetBlock(position) != null && GetBlock(position).HasShipComponent())
            {
                GetBlock(position).DeleteShipComponent();
                return true;
            }

            return false;
        }
    }
}
