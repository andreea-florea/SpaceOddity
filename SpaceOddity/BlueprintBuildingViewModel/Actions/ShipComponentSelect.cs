using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Actions
{
    public class ShipComponentSelect : IAction
    {
        private IController controller;
        private Coordinate position;

        public ShipComponentSelect(IController controller, Coordinate position)
        {
            this.controller = controller;
            this.position = position;
        }

        public void Execute()
        {
            controller.SelectShipComponent(position);
        }
    }
}
