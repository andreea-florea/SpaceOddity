using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.MenuControls
{
    public class ToggleAction : IAction
    {
        private IDropDownList dropDownList;

        public ToggleAction(IDropDownList dropDownList)
        {
            this.dropDownList = dropDownList;
        }

        public void Execute()
        {
            dropDownList.Toggle();
        }
    }
}
