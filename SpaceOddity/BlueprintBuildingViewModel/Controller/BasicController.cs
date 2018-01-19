using Game;
using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueprintBuildingViewModel.DataStructures;

namespace BlueprintBuildingViewModel.Controller
{
    public class BasicController : IController
    {
        private MasterController masterController;
        private PipeBuildController pipeBuildController;
        private IBlueprintBuilder blueprintBuilder;

        public BasicController(MasterController masterController,
            PipeBuildController pipeBuildController,
            IBlueprintBuilder blueprintBuilder)
        {
            this.masterController = masterController;
            this.pipeBuildController = pipeBuildController;
            this.blueprintBuilder = blueprintBuilder;
        }

        public void SelectTile(Coordinate position)
        {
            blueprintBuilder.CreateBlock(position);
        }

        public void SelectBlock(Coordinate position)
        {
            blueprintBuilder.AddShipComponent(position);
        }

        public void CancelBlock(Coordinate position)
        {
            blueprintBuilder.DeleteBlock(position);
        }

        public void SelectShipComponent(Coordinate position)
        {
        }

        public void CancelShipComponent(Coordinate position)
        {
            blueprintBuilder.DeleteShipComponent(position);
        }

        public void SelectPipeLink(CoordinatePair edge)
        {
            pipeBuildController.SelectedLink = edge;
            masterController.CurrentController = pipeBuildController;
        }
    }
}
