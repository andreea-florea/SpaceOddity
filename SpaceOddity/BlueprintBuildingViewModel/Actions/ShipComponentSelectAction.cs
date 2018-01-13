using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Actions
{
    public class ShipComponentSelectAction : IAction
    {
        private IBlueprintBuilderController controller;
        private Coordinate position;

        public ShipComponentSelectAction(IBlueprintBuilderController controller, Coordinate position)
        {
            this.controller = controller;
            this.position = position;
        }

        public void Execute()
        {
            controller.ShipComponentSelect(position);
        }
    }
}
