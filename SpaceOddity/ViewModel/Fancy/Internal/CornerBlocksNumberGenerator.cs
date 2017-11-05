using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Fancy.Iternal
{
    internal class CornerBlocksNumberGenerator : IBitNumberGenerator
    {
        private IBlueprintBuilder blueprintBuilder;

        public CornerBlocksNumberGenerator(IBlueprintBuilder blueprintBuilder)
        {
            this.blueprintBuilder = blueprintBuilder;
        }

        public bool[] GenerateNumber(Coordinate position, Coordinate facing)
        {
            var rightDirection = facing.RotateQuarterCircleRight();
            return new bool[] {
                blueprintBuilder.HasBlock(position + facing),
                blueprintBuilder.HasBlock(position + rightDirection),
                blueprintBuilder.HasBlock(position + facing + rightDirection)
            };
        }
    }
}
