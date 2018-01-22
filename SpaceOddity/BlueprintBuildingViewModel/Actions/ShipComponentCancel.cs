using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Actions
{
    public class ShipComponentCancel : IAction
    {
        private IController controller;
        private Coordinate position;

        public ShipComponentCancel(IController controller, Coordinate position)
        {
            this.controller = controller;
            this.position = position;
        }

        public void Execute()
        {
            controller.CancelShipComponent(position);
        }
    }
}
