﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;

namespace WpfView
{
    public interface IFrameworkElementFactory
    {
        FrameworkElement CreateElement();
    }
}