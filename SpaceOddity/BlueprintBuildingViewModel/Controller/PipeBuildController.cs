using Game;
using Game.Enums;
using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueprintBuildingViewModel.DataStructures;
using BlueprintBuildingViewModel.Extensions;

namespace BlueprintBuildingViewModel.Controller
{
    public class PipeBuildController : IController
    {
        private MasterController masterController;
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

        public PipeBuildController(MasterController masterController, 
            IBlueprintBuilder blueprintBuilder, CoordinatePair selectedLink)
        {
            this.masterController = masterController;
            this.blueprintBuilder = blueprintBuilder;
            this.selectedLink = selectedLink;
        }

        public void SelectTile(Coordinate position)
        {
        }

        public void SelectBlock(Coordinate position)
        {
        }

        public void CancelBlock(Coordinate position)
        {
        }

        public void SelectShipComponent(Coordinate position)
        {
            if (SelectedLink.Positions.Contains(position))
            {
                var pipeEdge = GetOtherPositionDirection(position, SelectedLink);
                if (PipeExists(position, pipeEdge))
                {
                    blueprintBuilder.DeleteConnectingPipe(position, new ConnectingPipe(pipeEdge));
                }
                else
                {
                    blueprintBuilder.AddConnectingPipe(position, pipeEdge);
                }
            }
            masterController.Reset();
        }

        public void CancelShipComponent(Coordinate position)
        {

        }

        public void SelectPipeLink(CoordinatePair edge)
        {
            var commonPosition = edge.GetCommonPosition(SelectedLink);
            if (commonPosition.IsFound && edge != SelectedLink)
            {
                AddOrDeletePipe(commonPosition.Element, SelectedLink, edge);
            }
            masterController.Reset();
        }

        private void AddOrDeletePipe(Coordinate position, CoordinatePair edge, CoordinatePair otherEdge)
        {
            var firstDirection = GetOtherPositionDirection(position, edge);
            var secondDirection = GetOtherPositionDirection(position, otherEdge);
            AddOrDeletePipe(position, new DoubleEdgedPipe(firstDirection, secondDirection));
        }

        private void AddOrDeletePipe(Coordinate position, DoubleEdgedPipe pipe)
        {
            if (PipeExists(position, pipe) || PipeExists(position, pipe.FirstEdge) && PipeExists(position, pipe.SecondEdge))
            {
                blueprintBuilder.DeleteDoubleEdgedPipe(position, pipe);
            }
            else
            {
                blueprintBuilder.AddDoubleEdgedPipe(position, pipe.FirstEdge, pipe.SecondEdge);
            }
        }

        private bool PipeExists(Coordinate position, DoubleEdgedPipe pipe)
        {
            return blueprintBuilder.GetBlock(position).PipesWithBothEdges.Any(blockPipe => blockPipe.IsEqualTo(pipe));
        }

        private bool PipeExists(Coordinate position, EdgeType edge)
        {
            return blueprintBuilder.GetBlock(position).PipesWithOneEdge.Any(blockPipe => blockPipe.Edge == edge);
        }

        private EdgeType GetOtherPositionDirection(Coordinate position, CoordinatePair edge)
        {
            return (edge.First - position + edge.Second - position).ToEdgeType();
        }
    }
}
