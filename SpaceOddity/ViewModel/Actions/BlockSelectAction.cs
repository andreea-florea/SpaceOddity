using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.Actions
{
    internal class BlockSelectAction : IAction
    {
        private IBlueprintBuilderController controller;
        private Coordinate position;

        public BlockSelectAction(IBlueprintBuilderController controller, Coordinate position)
        {
            this.controller = controller;
            this.position = position;
        }

        public void Execute()
        {
            controller.BlockSelect(position);
        }
    }
}
