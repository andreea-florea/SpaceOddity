using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.Fancy.Iternal
{
    internal class WorldObjectBitNumberFactoryPicker : IFacingContextWorldObjectFactory
    {
        private IWorldObjectFactory[] factories;
        private IBitNumberGenerator numberGenerator;

        public WorldObjectBitNumberFactoryPicker(IWorldObjectFactory[] factories, IBitNumberGenerator numberGenerator)
        {
            this.factories = factories;
            this.numberGenerator = numberGenerator;
        }

        public IWorldObject CreateObject(Coordinate position, Coordinate facing)
        {
            var index = ConvertBoolArrayToInt(numberGenerator.GenerateNumber(position, facing));
            return factories[index].CreateObject();
        }

        private int ConvertBoolArrayToInt(bool[] bits)
        {
            var bitPower = 1;
            var value = 0;
            for (var i = 0; i < bits.Length; ++i)
            {
                value += bits[i] ? bitPower : 0;
                bitPower <<= 1;
            }
            Console.WriteLine(value);
            return value;
        }
    }
}
