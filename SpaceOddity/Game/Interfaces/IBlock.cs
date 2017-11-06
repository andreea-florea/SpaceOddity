﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlock
    {
        double Weight { get; }

        IShipComponent ShipComponent { get; }

        List<DoubleEdgedPipe> PipesWithBothEdges { get; set; }
        List<ConnectingPipe> PipesWithOneEdge { get; }

        void AddShipComponent(IShipComponent component);

        void DeleteShipComponent();

        bool HasShipComponent();
    }
}
