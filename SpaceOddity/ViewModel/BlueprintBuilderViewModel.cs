using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class BlueprintBuilderViewModel : IBlueprintBuilderObserver
    {
        private IWorldObject[,] tiles;
        private IWorldObject[,] blocks;

        private IWorldObjectFactory blockFactory;

        public int Width
        {
            get
            {
                return tiles.GetLength(1);
            }
        }

        public int Height
        {
            get
            {
                return tiles.GetLength(0);
            }
        }

        public BlueprintBuilderViewModel(IWorldObject[,] tiles, IWorldObject[,] blocks, IWorldObjectFactory blockFactory)
        {
            this.tiles = tiles;
            this.blocks = blocks;
            this.blockFactory = blockFactory;
        }

        public IWorldObject GetTile(Coordinate position)
        {
            return tiles.Get(position);
        }

        public IWorldObject GetBlock(Coordinate position)
        {
            return blocks.Get(position);
        }

        public void BlockCreated(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            blocks.Set(position, blockFactory.CreateObject());
            blocks.Get(position).Position = tiles.Get(position).Position;
            blocks.Get(position).Scale = tiles.Get(position).Scale;
        }

        public void ErrorBlockNotCreated(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void BlockDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ErrorBlockNotDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ShipComponentAdded(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ErrorShipComponentNotAdded(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ShipComponentDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ErrorShipComponentNotDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }
    }
}
