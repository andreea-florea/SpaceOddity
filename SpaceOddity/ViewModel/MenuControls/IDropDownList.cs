using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.MenuControls
{
    public interface IDropDownList
    {
        int SelectedIndex { get; set; }
        void Toggle();
        void Close();
    }
}
