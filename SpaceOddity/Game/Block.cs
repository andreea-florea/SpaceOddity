using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Interfaces;
using NaturalNumbersMath;

namespace Game
{
    public class Block : IBlock
    {
        public double Weight { get; private set; }
        public IShipComponent ShipComponent { get; private set; }
        public Coordinate Position { get; private set; }

        private List<DoubleEdgedPipe> pipesWithBothEdges;
        private List<ConnectingPipe> pipesWithOneEdge;

        public IEnumerable<DoubleEdgedPipe> PipesWithBothEdges
        {
            get
            {
                return pipesWithBothEdges;
            }
        }

        public IEnumerable<ConnectingPipe> PipesWithOneEdge
        {
            get
            {
                return pipesWithOneEdge;
            }
        }

        public Block(double weight, List<DoubleEdgedPipe> pipesWithBothEdges, List<ConnectingPipe> pipesWithOneEdge)
        {
            Weight = weight;
            this.pipesWithBothEdges = pipesWithBothEdges;
            this.pipesWithOneEdge = pipesWithOneEdge;
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

        public void AddPipe(DoubleEdgedPipe pipe)
        {
            pipesWithBothEdges.Add(pipe);
        }

        public void AddPipe(ConnectingPipe pipe)
        {
            pipesWithOneEdge.Add(pipe);
        }

        public void DeletePipe(DoubleEdgedPipe pipe)
        {
            pipesWithBothEdges.Remove(pipe);
        }

        public void DeletePipe(ConnectingPipe pipe)
        {
            pipesWithOneEdge.Remove(pipe);
        }

        public void SetPosition(Coordinate position)
        {
            Position = position;
        }
    }
}
