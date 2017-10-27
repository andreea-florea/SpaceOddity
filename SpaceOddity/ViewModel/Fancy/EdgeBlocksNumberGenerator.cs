using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Fancy
{
    public class EdgeBlocksNumberGenerator : IBitNumberGenerator
    {
        private IBlueprintBuilder blueprintBuilder;

        public EdgeBlocksNumberGenerator(IBlueprintBuilder blueprintBuilder)
        {
            this.blueprintBuilder = blueprintBuilder;
        }

        public bool[] GenerateNumber(Coordinate position, Coordinate facing)
        {
            return new bool[1] { HasBlock(position + facing) };
        }

        private bool HasBlock(Coordinate position)
        {
            return blueprintBuilder.GetBlock(position.Y, position.X) != null;
        }
    }
}
