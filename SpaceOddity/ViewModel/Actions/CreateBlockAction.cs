using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.Actions
{
    public class CreateBlockAction : IAction
    {
        private IBlueprintBuilder blueprintBuilder;
        private Coordinate position;

        public CreateBlockAction(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            this.blueprintBuilder = blueprintBuilder;
            this.position = position;
        }

        public void Execute()
        {
            blueprintBuilder.CreateBlock(position);
        }
    }
}
