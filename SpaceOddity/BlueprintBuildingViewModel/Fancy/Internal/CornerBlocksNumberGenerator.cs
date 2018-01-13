using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueprintBuildingViewModel.Fancy.Iternal
{
    internal class CornerBlocksNumberGenerator : IBitNumberGenerator
    {
        private IBlueprintBuilder blueprintBuilder;

        public CornerBlocksNumberGenerator(IBlueprintBuilder blueprintBuilder)
        {
            this.blueprintBuilder = blueprintBuilder;
        }

        public bool[] GenerateNumber(FacingPosition position)
        {
            return new bool[] {
                blueprintBuilder.HasBlock(position.Position + position.Forward),
                blueprintBuilder.HasBlock(position.Position + position.Right),
                blueprintBuilder.HasBlock(position.Position + position.Forward + position.Right)
            };
        }
    }
}
