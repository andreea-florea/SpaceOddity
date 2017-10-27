﻿using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.Fancy
{
    public class IgnoreFacingContextWorldObjectFactory : IFacingContextWorldObjectFactory
    {
        private IWorldObjectFactory baseFactory;

        public IgnoreFacingContextWorldObjectFactory(IWorldObjectFactory baseFactory)
        {
            this.baseFactory = baseFactory;
        }
        
        public IWorldObject CreateObject(Coordinate position, Coordinate facing)
        {
            return baseFactory.CreateObject();
        }
    }
}
