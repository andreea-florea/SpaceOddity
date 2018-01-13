using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.Controller;
using BlueprintBuildingViewModel.DataStructures;

namespace BlueprintBuildingViewModel.Actions
{
    public class PipeLinkSelectAction : IAction
    {
        private IBlueprintBuilderController controller;
        private CoordinatePair edge;

        public PipeLinkSelectAction(IBlueprintBuilderController controller, CoordinatePair edge)
        {
            this.controller = controller;
            this.edge = edge;
        }

        public void Execute()
        {
            controller.PipeLinkSelect(edge);
        }
    }
}
