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
using ViewModel.Extensions;
using Algorithm;
using Game;

namespace ViewModel
{
    public class BlueprintBuilderViewModel : IBlueprintObserver
    {
        private BlueprintBuilderObjectTable objectTable;

        private IFactory<IBuilderWorldObject> blockFactory;
        private IFactory<IBuilderWorldObject> shipComponentFactory;
        private IFactory<IBuilderWorldObject> pipeLinkFactory;
        private IFactory<IWorldObject, ICurve> pipeFactory;

        private IBlueprintBuilderControlAssigner controller;

        public BlueprintBuilderViewModel(BlueprintBuilderObjectTable objectTable,
            IFactory<IBuilderWorldObject> blockFactory,
            IFactory<IBuilderWorldObject> shipComponentFactory,
            IFactory<IBuilderWorldObject> pipeLinkFactory,
            IFactory<IWorldObject, ICurve> pipeFactory,
            IBlueprintBuilderControlAssigner controller)
        {
            this.objectTable = objectTable;
            this.blockFactory = blockFactory;
            this.shipComponentFactory = shipComponentFactory;
            this.pipeLinkFactory = pipeLinkFactory;
            this.pipeFactory = pipeFactory;
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
            controller.AssignShipComponentControl(objectTable.GetShipComponent(position), position);
        }

        public void ShipComponentDeleted(IBlueprint blueprint, Coordinate position)
        {
        }

        public void DoubleEdgePipeAdded(IBlueprint blueprint, Coordinate position, DoubleEdgedPipe pipe)
        {
            CreatePipeObject(blueprint, position, pipe.FirstEdge, pipe.SecondEdge);
        }

        public void ConnectingPipeAdded(IBlueprint blueprint, Coordinate position, ConnectingPipe pipe)
        {
            CreatePipeObject(blueprint, position, pipe.Edge, EdgeType.COUNT);
        }

        private void CreatePipeObject(IBlueprint blueprint, Coordinate position, EdgeType firstEdge, EdgeType secondEdge)
        {
            var curve = CreatePipeCurve(firstEdge, secondEdge);
            var pipeObject = pipeFactory.Create(curve);
            pipeObject.Position = objectTable.GetTile(position).Position;
            pipeObject.Scale = objectTable.GetTile(position).Scale;
            objectTable.SetPipe(position, firstEdge, secondEdge, pipeObject);
        }

        private ICurve CreatePipeCurve(EdgeType firstEdge, EdgeType secondEdge)
        {
            var firstPosition = firstEdge.ToCoordinate().ToVector2();
            var secondPosition = secondEdge.ToCoordinate().ToVector2();
            return new StraightLineCurve(firstPosition, secondPosition - firstPosition);
        }

        public void DoubleEdgePipeDeleted(IBlueprint blueprint, Coordinate position, DoubleEdgedPipe pipe)
        {
            objectTable.DeletePipe(position, pipe.FirstEdge, pipe.SecondEdge);
        }

        public void ConnectingPipeDeleted(IBlueprint blueprint, Coordinate position, ConnectingPipe pipe)
        {
            objectTable.DeletePipe(position, pipe.Edge, EdgeType.COUNT);
        }
    }
}
