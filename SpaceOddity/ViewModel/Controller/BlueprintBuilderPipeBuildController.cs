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

        public CoordinatePair SelectedLink { get; set; }

        public BlueprintBuilderPipeBuildController(BlueprintBuilderMasterController masterController, 
            IBlueprintBuilder blueprintBuilder, CoordinatePair selectedLink)
        {
            this.masterController = masterController;
            this.blueprintBuilder = blueprintBuilder;
            this.SelectedLink = selectedLink;
        }

        public void TileSelect(Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void BlockSelect(Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void BlockCancel(Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void PipeLinkSelect(CoordinatePair edge)
        {
            var commonPosition = edge.GetCommonPosition(SelectedLink);
            if (commonPosition.IsFound)
            {
                var position = commonPosition.Element;
                var firstDirection = GetOtherPositionDirection(position, SelectedLink);
                var secondDirection = GetOtherPositionDirection(position, edge);
                blueprintBuilder.AddDoubleEdgedPipe(position, firstDirection.GetEdgeType(), secondDirection.GetEdgeType());
            }
            masterController.Reset();
        }

        private Coordinate GetOtherPositionDirection(Coordinate position, CoordinatePair edge)
        {
            return edge.First - position + edge.Second - position;
        }
    }
}
