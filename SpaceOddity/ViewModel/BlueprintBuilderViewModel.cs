using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using Geometry;

namespace ViewModel
{
    public class BlueprintBuilderViewModel : IBlueprintBuilderObserver
    {
        private IWorldObject[,] tiles;
        private IWorldObject[,] blocks;
        private IWorldObject[,] shipComponents;
        private IWorldObject[,] horizontalPipeLinks;
        private IWorldObject[,] verticalPipeLinks;

        private IWorldObjectFactory blockFactory;
        private IWorldObjectFactory shipComponentFactory;
        private IWorldObjectFactory pipeLinkFactory;

        private IBlueprintBuilderControlAssigner controller;

        public BlueprintBuilderViewModel(
            IWorldObject[,] tiles, 
            IWorldObject[,] blocks,
            IWorldObject[,] shipComponents, 
            IWorldObject[,] horizontalPipeLinks, 
            IWorldObject[,] verticalPipeLinks,
            IWorldObjectFactory blockFactory, 
            IWorldObjectFactory shipComponentFactory, 
            IWorldObjectFactory pipeLinkFactory,
            IBlueprintBuilderControlAssigner controller)
        {
            this.tiles = tiles;
            this.blocks = blocks;
            this.shipComponents = shipComponents;
            this.horizontalPipeLinks = horizontalPipeLinks;
            this.verticalPipeLinks = verticalPipeLinks;
            this.blockFactory = blockFactory;
            this.shipComponentFactory = shipComponentFactory;
            this.pipeLinkFactory = pipeLinkFactory;
            this.controller = controller;
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
            CreateBlock(position);
            UpdatePipeLinks(blueprintBuilder, position);
        }

        private void CreateBlock(Coordinate position)
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
            DeleteObject(blocks, position);
            UpdatePipeLinks(blueprintBuilder, position);
        }

        private void UpdatePipeLinks(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            UpdatePipeLink(horizontalPipeLinks, blueprintBuilder, position, Coordinates.Up);
            UpdatePipeLink(horizontalPipeLinks, blueprintBuilder, position, Coordinates.Down);
            UpdatePipeLink(verticalPipeLinks, blueprintBuilder, position, Coordinates.Right);
            UpdatePipeLink(verticalPipeLinks, blueprintBuilder, position, Coordinates.Left);
        }

        private void UpdatePipeLink(IWorldObject[,] links, IBlueprintBuilder blueprintBuilder,
            Coordinate position, Coordinate direction)
        {
            var connectingPosition = position + direction;
            var updatePosition = new Coordinate(
                Math.Min(position.X, connectingPosition.X),
                Math.Min(position.Y, connectingPosition.Y));

            if (blueprintBuilder.HasBlock(connectingPosition) && blueprintBuilder.HasBlock(position))
            {
                links.Set(updatePosition, pipeLinkFactory.CreateObject());
                links.Get(updatePosition).Position = 
                    (tiles.Get(position).Position + tiles.Get(connectingPosition).Position) * 0.5;
                links.Get(updatePosition).Rotation = direction.ToVector2();
            }
            else
            {
                DeleteObject(links, updatePosition);
            }
        }

        private void DeleteObject(IWorldObject[,] array, Coordinate position)
        {
            if (array.Get(position) != null)
            {
                array.Get(position).Delete();
                array.Set(position, null);
            }
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
            //Fix after Blueprint splitting
            //throw new NotImplementedException();
        }

        public void ShipComponentDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ErrorShipComponentNotDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            //Fix after Blueprint splitting
            //throw new NotImplementedException();
        }
    }
}
