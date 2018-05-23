using Algorithms;
using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.MenuControls
{
    public class DropDownList : IDropDownList
    {
        private int selectedIndex;
        public int SelectedIndex 
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                selectedIndex = value;
                Object.Delete();
                CreateSelectedObject();
            }
        }

        public IWorldObject Object { get; private set; }
        private Vector2 position;
        private Vector2 scale;
        public Vector2 Direction { get; private set; }

        private WorldObjectList<IWorldObject> expandedItems;
        private IList<IFactory<IWorldObject>> itemFactories;

        public DropDownList(
            int selectedIndex, 
            Vector2 position,
            Vector2 scale,
            Vector2 direction,
            IList<IFactory<IWorldObject>> itemFactories)
        {
            this.selectedIndex = selectedIndex;
            this.position = position;
            this.scale = scale;
            this.Direction = direction;
            this.itemFactories = itemFactories;
            expandedItems = new WorldObjectList<IWorldObject>();
            CreateSelectedObject();
        }

        private void CreateSelectedObject()
        {
            Object = itemFactories[SelectedIndex].Create();
            Object.Position = position;
            Object.Scale = scale;
            Object.LeftClickAction = new ToggleAction(this);
        }

        public void Toggle()
        {
            if (expandedItems.Any())
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        public void Close()
        {
            expandedItems.Clear();
        }

        private void Open()
        {
            expandedItems.AddRange(itemFactories.Select(factory => factory.Create()));
            for (var i = 0; i < expandedItems.Count; ++i)
            {
                expandedItems[i].Position = position + Direction * (i + 1);
                expandedItems[i].Scale = scale;
                expandedItems[i].LeftClickAction = new SelectDropDownItemAction(this, i);
            }
        }
    }
}
