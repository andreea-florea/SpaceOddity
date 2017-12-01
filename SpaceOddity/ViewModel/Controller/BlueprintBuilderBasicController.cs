using Game;
using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.DataStructures;

namespace ViewModel.Controller
{
    public class BlueprintBuilderBasicController : IBlueprintBuilderController
    {
        private BlueprintBuilderMasterController masterController;
        private BlueprintBuilderPipeBuildController pipeBuildController;
        private IBlueprintBuilder blueprintBuilder;

        public BlueprintBuilderBasicController(BlueprintBuilderMasterController masterController,
            BlueprintBuilderPipeBuildController pipeBuildController,
            IBlueprintBuilder blueprintBuilder)
        {
            this.masterController = masterController;
            this.pipeBuildController = pipeBuildController;
            this.blueprintBuilder = blueprintBuilder;
        }

        public void TileSelect(Coordinate position)
        {
            blueprintBuilder.CreateBlock(position);
        }

        public void BlockSelect(Coordinate position)
        {
            blueprintBuilder.AddShipComponent(position);
        }

        public void BlockCancel(Coordinate position)
        {
            blueprintBuilder.DeleteBlock(position);
        }

        public void PipeLinkSelect(CoordinatePair edge)
        {
            pipeBuildController.SelectedLink = edge;
            masterController.CurrentController = pipeBuildController;
        }
    }
}
