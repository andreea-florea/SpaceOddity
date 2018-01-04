using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Actions;
using ViewModel.DataStructures;

namespace ViewModel.Controller
{
    public class BlueprintBuilderControlAssigner : IBlueprintBuilderControlAssigner
    {
        private IBlueprintBuilderController controller;

        public BlueprintBuilderControlAssigner(IBlueprintBuilderController controller)
        {
            this.controller = controller;
        }

        public void AssignTileControl(IWorldObject tile, Coordinate position)
        {
            tile.LeftClickAction = new TileSelectAction(controller, position);
        }

        public void AssignBlockControl(IWorldObject block, Coordinate position)
        {
            block.RightClickAction = new BlockCancelAction(controller, position);
            block.LeftClickAction = new BlockSelectAction(controller, position);
        }

        public void AssignPipeLinkControl(IWorldObject pipeLink, CoordinatePair edge)
        {
            pipeLink.LeftClickAction = new PipeLinkSelectAction(controller, edge);
        }

        public void AssignShipComponentControl(IWorldObject shipComponent, Coordinate position)
        {
            shipComponent.LeftClickAction = new ShipComponentSelectAction(controller, position);
        }
    }
}
