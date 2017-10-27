using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Fancy
{
    public class CornerBlocksNumberGenerator : IBitNumberGenerator
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
                HasBlock(position + facing),
                HasBlock(position + rightDirection),
                HasBlock(position + facing + rightDirection)
            };
        }

        private bool HasBlock(Coordinate position)
        {
            return blueprintBuilder.GetBlock(position) != null;
        }
    }
}
