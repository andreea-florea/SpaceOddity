using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using BlueprintBuildingViewModel.Controller;

namespace BlueprintBuildingViewModel.Actions
{
    internal class TileSelect : IAction
    {
        private IController controller;
        private Coordinate position;

        public TileSelect(IController controller, Coordinate position)
        {
            this.controller = controller;
            this.position = position;
        }

        public void Execute()
        {
            controller.SelectTile(position);
        }
    }
}
