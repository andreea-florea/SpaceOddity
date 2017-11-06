using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Interfaces;

namespace Game
{
    public class Block : IBlock
    {
        public double Weight { get; private set; }

        public IShipComponent ShipComponent { get; private set; }

        public List<DoubleEdgedPipe> PipesWithBothEdges { get; set; }
        public List<ConnectingPipe> PipesWithOneEdge { get; private set; }

        public Block(double weight, List<DoubleEdgedPipe> pipesWithBothEdges, List<ConnectingPipe> pipesWithOneEdge)
        {
            Weight = weight;
            PipesWithBothEdges = pipesWithBothEdges;
            PipesWithOneEdge = pipesWithOneEdge;
        }

        public void AddShipComponent(IShipComponent shipComponent)
        {
            ShipComponent = shipComponent;
        }

        public void DeleteShipComponent()
        {
            ShipComponent = null;
        }

        public bool HasShipComponent()
        {
            return (ShipComponent != null);
        }
    }
}
