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
    public class PipeLinkSelect : IAction
    {
        private IController controller;
        private CoordinatePair edge;

        public PipeLinkSelect(IController controller, CoordinatePair edge)
        {
            this.controller = controller;
            this.edge = edge;
        }

        public void Execute()
        {
            controller.SelectPipeLink(edge);
        }
    }
}
