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
            return tiles[position.Y, position.X];
        }

        public IWorldObject GetBlock(Coordinate position)
        {
            return blocks[position.Y, position.X];
        }

        public void BlockCreated(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            blocks[position.Y, position.X] = blockFactory.CreateObject();
            blocks[position.Y, position.X].Position = tiles[position.Y, position.X].Position;
            blocks[position.Y, position.X].Scale = tiles[position.Y, position.X].Scale;
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
