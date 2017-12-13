using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using Geometry;
using ViewModel.Actions;
using ViewModel.DataStructures;
using ViewModel.Controller;
using Algorithm;

namespace ViewModel
{
    public class BlueprintBuilderViewModel : IBlueprintObserver
    {
        private BlueprintBuilderObjectTable objectTable;

        private IFactory<IBuilderWorldObject> blockFactory;
        private IFactory<IBuilderWorldObject> shipComponentFactory;
        private IFactory<IBuilderWorldObject> pipeLinkFactory;

        private IBlueprintBuilderControlAssigner controller;

        public BlueprintBuilderViewModel(BlueprintBuilderObjectTable objectTable,
            IFactory<IBuilderWorldObject> blockFactory,
            IFactory<IBuilderWorldObject> shipComponentFactory,
            IFactory<IBuilderWorldObject> pipeLinkFactory,
            IBlueprintBuilderControlAssigner controller)
        {
            this.objectTable = objectTable;
            this.blockFactory = blockFactory;
            this.shipComponentFactory = shipComponentFactory;
            this.pipeLinkFactory = pipeLinkFactory;
            this.controller = controller;
        }

        public IWorldObject GetTile(Coordinate position)
        {
            return objectTable.GetTile(position);
        }

        public IWorldObject GetBlock(Coordinate position)
        {
            return objectTable.GetBlock(position);
        }

        public void BlockCreated(IBlueprint blueprint, Coordinate position)
        {
            CreateBlock(position);
            UpdatePipeLinks(blueprint, position);
        }

        private void CreateBlock(Coordinate position)
        {
            objectTable.SetBlock(position, blockFactory.Create());
            objectTable.GetBlock(position).Position = objectTable.GetTile(position).Position;
            objectTable.GetBlock(position).Scale = objectTable.GetTile(position).Scale;
            controller.AssignBlockControl(objectTable.GetBlock(position), position);
        }

        public void BlockDeleted(IBlueprint blueprint, Coordinate position)
        {
            objectTable.DeleteBlock(position);
            UpdatePipeLinks(blueprint, position);
        }

        private void UpdatePipeLinks(IBlueprint blueprint, Coordinate position)
        {
            UpdatePipeLink(blueprint, position, Coordinates.Up);
            UpdatePipeLink(blueprint, position, Coordinates.Down);
            UpdatePipeLink(blueprint, position, Coordinates.Right);
            UpdatePipeLink(blueprint, position, Coordinates.Left);
        }

        private void UpdatePipeLink(IBlueprint blueprint, Coordinate position, Coordinate direction)
        {
            var edge = new CoordinatePair(position, position + direction);

            if (edge.Positions.All(pos => blueprint.HasBlock(pos)))
            {
                objectTable.SetPipeLink(edge, pipeLinkFactory.Create());
                objectTable.GetPipeLink(edge).Position = GetCenter(edge);
                objectTable.GetPipeLink(edge).Rotation = direction.ToVector2();
                controller.AssignPipeLinkControl(objectTable.GetPipeLink(edge), edge);
            }
            else
            {
                objectTable.DeletePipeLink(edge);
            }
        }

        private Vector2 GetCenter(CoordinatePair edge)
        {
            return (objectTable.GetTile(edge.First).Position + objectTable.GetTile(edge.Second).Position) * 0.5;
        }

        public void ShipComponentAdded(IBlueprint blueprint, Coordinate position)
        {
            objectTable.SetShipComponent(position, shipComponentFactory.Create());
            objectTable.GetShipComponent(position).Position = objectTable.GetTile(position).Position;
            objectTable.GetShipComponent(position).Scale = objectTable.GetTile(position).Scale;
        }

        public void ShipComponentDeleted(IBlueprint blueprint, Coordinate position)
        {
        }

        public void DoubleEdgePipeAdded(IBlueprint blueprint, Coordinate position)
        {
        }

        public void DoubleEdgePipeDeleted(IBlueprint blueprint, Coordinate position)
        {
        }

        public void ConnectingPipeAdded(IBlueprint blueprint, Coordinate position)
        {
        }

        public void ConnectingPipeDeleted(IBlueprint blueprint, Coordinate position)
        {
        }
    }
}
