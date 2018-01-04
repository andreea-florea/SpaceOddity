using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Controller;

namespace ViewModel.Actions
{
    public class ShipComponentCancelAction : IAction
    {
        private IBlueprintBuilderController controller;
        private Coordinate position;

        public ShipComponentCancelAction(IBlueprintBuilderController controller, Coordinate position)
        {
            this.controller = controller;
            this.position = position;
        }

        public void Execute()
        {
            controller.ShipComponentCancel(position);
        }
    }
}
