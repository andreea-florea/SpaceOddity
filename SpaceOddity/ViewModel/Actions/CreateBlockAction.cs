using Game.Interfaces;
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
        private int x;
        private int y;

        public CreateBlockAction(IBlueprintBuilder blueprintBuilder, int x, int y)
        {
            this.blueprintBuilder = blueprintBuilder;
            this.x = x;
            this.y = y;
        }

        public void Execute()
        {
            blueprintBuilder.CreateBlock(y, x);
        }
    }
}
