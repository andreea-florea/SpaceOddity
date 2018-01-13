using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueprintBuildingViewModel.Fancy.Iternal
{
    internal class EdgeBlocksNumberGenerator : IBitNumberGenerator
    {
        private IBlueprintBuilder blueprintBuilder;

        public EdgeBlocksNumberGenerator(IBlueprintBuilder blueprintBuilder)
        {
            this.blueprintBuilder = blueprintBuilder;
        }

        public bool[] GenerateNumber(FacingPosition position)
        {
            return new bool[1] { blueprintBuilder.HasBlock(position.Position + position.Forward) };
        }
    }
}
