﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Interfaces
{
    public interface IWorldObjectFactory
    {
        IWorldObject CreateObject();
    }
}
