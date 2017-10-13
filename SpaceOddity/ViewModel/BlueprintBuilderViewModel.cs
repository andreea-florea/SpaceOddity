using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public IWorldObject GetTile(int x, int y)
        {
            return tiles[y, x];
        }

        public IWorldObject GetBlock(int x, int y)
        {
            return blocks[y, x];
        }

        public void BlockCreated(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            blocks[y, x] = blockFactory.CreateObject();
            blocks[y, x].Position = tiles[y, x].Position;
            blocks[y, x].Scale = tiles[y, x].Scale;
        }

        public void ErrorBlockNotCreated(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void BlockDeleted(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void ErrorBlockNotDeleted(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void ShipComponentAdded(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void ErrorShipComponentNotAdded(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void ShipComponentDeleted(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void ErrorShipComponentNotDeleted(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }
    }
}
