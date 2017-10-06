using Game.Interfaces;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class BlueprintBuilderViewModelFactory
    {
        private IWorldObjectFactory slotFactory;

        public BlueprintBuilderViewModelFactory(IWorldObjectFactory slotFactory)
        {
            this.slotFactory = slotFactory;
        }

        public BlueprintBuilderViewModel CreateViewModel(IObservableBlueprintBuilder builder, Rectangle fittingRectangle)
        {
            var slots = new IWorldObject[builder.Height, builder.Width];
            var slotRects = fittingRectangle.Split(builder.Width, builder.Height);

            for (var i = 0; i < builder.Height; ++i)
            {
                for (var j = 0; j < builder.Width; ++j)
                {
                    slots[i, j] = slotFactory.CreateObject();
                    slots[i, j].Position = slotRects[i, j].Center;
                }
            }

            return new BlueprintBuilderViewModel(slots);
        }
    }
}
