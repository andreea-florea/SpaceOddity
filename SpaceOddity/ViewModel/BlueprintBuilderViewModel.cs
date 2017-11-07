using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel
{
    public class BlueprintBuilderViewModel : IBlueprintBuilderObserver
    {
        private IWorldObject[,] tiles;
        private IWorldObject[,] blocks;
        private IWorldObject[,] shipComponents;

        private IWorldObjectFactory blockFactory;
        private IBlueprintBuilderControlAssigner controller;
        private IWorldObjectFactory shipComponentFactory;

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

        public BlueprintBuilderViewModel(IWorldObject[,] tiles, IWorldObject[,] blocks,
            IWorldObject[,] shipComponents, IWorldObjectFactory blockFactory, 
            IWorldObjectFactory shipComponentFactory, IBlueprintBuilderControlAssigner controller)
        {
            this.tiles = tiles;
            this.blocks = blocks;
            this.shipComponents = shipComponents;
            this.blockFactory = blockFactory;
            this.controller = controller;
            this.shipComponentFactory = shipComponentFactory;
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
            controller.AssignBlockControl(blocks.Get(position), position);
        }

        public void ErrorBlockNotCreated(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void BlockDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            blocks.Get(position).Delete();
            blocks.Set(position, null);
        }

        public void ErrorBlockNotDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ShipComponentAdded(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            shipComponents.Set(position, shipComponentFactory.CreateObject());
            shipComponents.Get(position).Position = tiles.Get(position).Position;
            shipComponents.Get(position).Scale = tiles.Get(position).Scale;
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
