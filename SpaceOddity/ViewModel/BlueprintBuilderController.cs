using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel
{
    public class BlueprintBuilderController : IBlueprintBuilderController
    {
        private IBlueprintBuilder blueprintBuilder;

        public BlueprintBuilderController(IBlueprintBuilder blueprintBuilder)
        {
            this.blueprintBuilder = blueprintBuilder;
        }

        public void TileSelect(Coordinate position)
        {
            blueprintBuilder.CreateBlock(position);
        }

        public void BlockCancel(Coordinate position)
        {
            blueprintBuilder.DeleteBlock(position);
        }
    }
}
