using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace WpfView
{
    public class ParentFrameworkElementWrapper : IFrameworkElementWrapper
    {
        private IFrameworkElementWrapper baseWrapper;
        private FrameworkElement panel;

        public FrameworkElement Element
        {
            get 
            {
                return panel;
            }
        }

        public ColorVector Fill
        {
            set 
            {
                baseWrapper.Fill = value;
            }
        }

        public ColorVector Border
        {
            set
            {
                baseWrapper.Border = value;            
            }
        }

        public ParentFrameworkElementWrapper(IFrameworkElementWrapper baseWrapper, FrameworkElement panel)
        {
            this.baseWrapper = baseWrapper;
            this.panel = panel;
        }
    }
}
