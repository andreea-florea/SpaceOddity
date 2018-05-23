using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.MenuControls
{
    public class SelectDropDownItemAction : IAction
    {
        private IDropDownList dropDownList;
        private int itemIndex;

        public SelectDropDownItemAction(IDropDownList dropDownList, int itemIndex)
        {
            this.dropDownList = dropDownList;
            this.itemIndex = itemIndex;
        }

        public void Execute()
        {
            dropDownList.SelectedIndex = itemIndex;
            dropDownList.Close();
        }
    }
}
