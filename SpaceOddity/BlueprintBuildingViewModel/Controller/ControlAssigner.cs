using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.Actions;
using BlueprintBuildingViewModel.DataStructures;
using ViewModel;

namespace BlueprintBuildingViewModel.Controller
{
    public class ControlAssigner : IControlAssigner
    {
        private IController controller;

        public ControlAssigner(IController controller)
        {
            this.controller = controller;
        }

        public void AssignTileControl(IWorldObject tile, Coordinate position)
        {
            tile.LeftClickAction = new TileSelect(controller, position);
        }

        public void AssignBlockControl(IWorldObject block, Coordinate position)
        {
            block.RightClickAction = new BlockCancel(controller, position);
            block.LeftClickAction = new BlockSelect(controller, position);
        }

        public void AssignPipeLinkControl(IWorldObject pipeLink, CoordinatePair edge)
        {
            pipeLink.LeftClickAction = new PipeLinkSelect(controller, edge);
        }

        public void AssignShipComponentControl(IWorldObject shipComponent, Coordinate position)
        {
            shipComponent.LeftClickAction = new ShipComponentSelect(controller, position);
            shipComponent.RightClickAction = new ShipComponentCancel(controller, position);
        }
    }
}
