using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.DataStructures;
using ViewModel.Extensions;

namespace ViewModel.Controller
{
    public class BlueprintBuilderPipeBuildController : IBlueprintBuilderController
    {
        private BlueprintBuilderMasterController masterController;
        private IBlueprintBuilder blueprintBuilder;

        private CoordinatePair selectedLink;
        public CoordinatePair SelectedLink 
        { 
            get
            {
                return selectedLink;
            }
            set
            {
                selectedLink = value;
                masterController.ActivatePipeLink(selectedLink);
            }
        }

        public BlueprintBuilderPipeBuildController(BlueprintBuilderMasterController masterController, 
            IBlueprintBuilder blueprintBuilder, CoordinatePair selectedLink)
        {
            this.masterController = masterController;
            this.blueprintBuilder = blueprintBuilder;
            this.selectedLink = selectedLink;
        }

        public void TileSelect(Coordinate position)
        {
        }

        public void BlockSelect(Coordinate position)
        {
        }

        public void BlockCancel(Coordinate position)
        {
        }

        public void PipeLinkSelect(CoordinatePair edge)
        {
            var commonPosition = edge.GetCommonPosition(SelectedLink);
            if (commonPosition.IsFound && edge != SelectedLink)
            {
                var position = commonPosition.Element;
                var firstDirection = GetOtherPositionDirection(position, SelectedLink);
                var secondDirection = GetOtherPositionDirection(position, edge);
                blueprintBuilder.AddDoubleEdgedPipe(position, firstDirection.ToEdgeType(), secondDirection.ToEdgeType());
            }
            masterController.Reset();
        }

        private Coordinate GetOtherPositionDirection(Coordinate position, CoordinatePair edge)
        {
            return edge.First - position + edge.Second - position;
        }
    }
}
