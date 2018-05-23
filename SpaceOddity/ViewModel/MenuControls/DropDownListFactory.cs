using Algorithms;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.MenuControls
{
    public class DropDownListFactory : IFactory<DropDownList>
    {
        private IList<IFactory<IWorldObject>> itemsList;
        private Vector2 position;
        private Vector2 direction;
        private int selectedItem;

        public DropDownListFactory(
            IList<IFactory<IWorldObject>> itemsList,
            Vector2 position,
            Vector2 direction,
            int selectedItem)
        {
            this.itemsList = itemsList;
            this.position = position;
            this.direction = direction;
            this.selectedItem = selectedItem;
        }

        public DropDownList Create()
        {
            return new DropDownList(
                selectedItem, position, Vector2s.Diagonal * direction.Magnitude, direction, itemsList);
        }
    }
}
