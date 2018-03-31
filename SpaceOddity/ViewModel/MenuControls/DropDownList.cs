using Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.MenuControls
{
    public class DropDownList
    {
        public int SelectedItem { get; private set; }
        public IWorldObject Object { get; private set; }
        public WorldObjectList<IWorldObject> ExpandedItems { get; private set; }

        private IEnumerable<IFactory<IWorldObject>> itemFactories;

        public DropDownList(
            int selectedItem, 
            IWorldObject worldObject, 
            IEnumerable<IFactory<IWorldObject>> itemFactories)
        {
            this.SelectedItem = selectedItem;
            this.Object = worldObject;
            this.itemFactories = itemFactories;
            ExpandedItems = new WorldObjectList<IWorldObject>();
        }

        public void Toggle()
        {
            if (ExpandedItems.Any())
            {
                ExpandedItems.Clear();
            }
            else
            {
                ExpandedItems.AddRange(itemFactories.Select(factory => factory.Create()));
            }
        }
    }
}
