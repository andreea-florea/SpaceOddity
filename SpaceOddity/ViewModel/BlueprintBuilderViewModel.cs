using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Interfaces;

namespace ViewModel
{
    public class BlueprintBuilderViewModel
    {
        private IWorldObject[,] slots;

        public int Width
        {
            get
            {
                return slots.GetLength(1);
            }
        }

        public int Height
        {
            get
            {
                return slots.GetLength(0);
            }
        }

        public BlueprintBuilderViewModel(IWorldObject[,] slots)
        {
            this.slots = slots;
        }

        public IWorldObject GetSlot(int y, int x)
        {
            return slots[y, x];
        }
    }
}
