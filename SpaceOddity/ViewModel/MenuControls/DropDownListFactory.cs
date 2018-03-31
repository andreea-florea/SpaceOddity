using Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.MenuControls
{
    public class DropDownListFactory : IFactory<DropDownList>
    {
        private List<IFactory<IWorldObject>> itemsList;
        private int selectedItem;

        public DropDownListFactory(List<IFactory<IWorldObject>> itemsList, int selectedItem)
        {
            this.itemsList = itemsList;
            this.selectedItem = selectedItem;
        }

        public DropDownList Create()
        {
            return new DropDownList(selectedItem, itemsList[selectedItem].Create(), itemsList);
        }
    }
}
