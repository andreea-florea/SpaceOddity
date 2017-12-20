using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NaturalNumbersMath;

namespace Game
{
    public class Blueprint : IBlueprint
    {
        private IBlock[,] blocks;
        private List<IBlueprintObserver> observers;

        public Blueprint(IBlock[,] blocks)
        {
            this.blocks = blocks;
            observers = new List<IBlueprintObserver>();
        }

        public Coordinate Dimensions
        {
            get
            {
                return new Coordinate(blocks.Width(), blocks.Height());
            }
        }

        public void AttachObserver(IBlueprintObserver observer)
        {
            observers.Add(observer);
        }

        public IConstBlock GetBlock(Coordinate position)
        {
            return blocks.Get(position);
        }

        public bool HasBlock(Coordinate position)
        {
            return (blocks.IsWithinBounds(position) && GetBlock(position) != null);
        }

        public void PlaceBlock(Coordinate position, IBlock block)
        {
            blocks.Set(position, block);

            foreach (var observer in observers)
            {
                observer.BlockCreated(this, position);
            }
        }

        public void RemoveBlock(Coordinate position)
        {
            blocks.Set(position, null);

            foreach (var observer in observers)
            {
                observer.BlockDeleted(this, position);
            }
        }

        public void PlaceShipComponent(Coordinate position, IShipComponent shipComponent)
        {
            blocks.Get(position).AddShipComponent(shipComponent);

            foreach (var observer in observers)
            {
                observer.ShipComponentAdded(this, position);
            }
        }

        public void RemoveShipComponent(Coordinate position)
        {
            blocks.Get(position).DeleteShipComponent();

            foreach (var observer in observers)
            {
                observer.ShipComponentDeleted(this, position);
            }
        }

        public void PlacePipe(Coordinate position, DoubleEdgedPipe pipe)
        {
            blocks.Get(position).AddPipe(pipe);

            foreach (var observer in observers)
            {
                observer.DoubleEdgePipeAdded(this, position, pipe);
            }
        }

        public void PlacePipe(Coordinate position, ConnectingPipe pipe)
        {
            blocks.Get(position).AddPipe(pipe);

            foreach (var observer in observers)
            {
                observer.ConnectingPipeAdded(this, position, pipe);
            }
        }

        public void RemovePipe(Coordinate position, DoubleEdgedPipe pipe)
        {
            blocks.Get(position).DeletePipe(pipe);

            foreach (var observer in observers)
            {
                observer.DoubleEdgePipeDeleted(this, position, pipe);
            }
        }

        public void RemovePipe(Coordinate position, ConnectingPipe pipe)
        {
            blocks.Get(position).DeletePipe(pipe);

            foreach (var observer in observers)
            {
                observer.ConnectingPipeDeleted(this, position, pipe);
            }
        }
    }
}
