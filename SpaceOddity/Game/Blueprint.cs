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

        public Blueprint(IBlock[,] blocks)
        {
            this.blocks = blocks;
        }

        public Coordinate Dimensions
        {
            get
            {
                return new Coordinate(blocks.Width(), blocks.Height());
            }
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
        }

        public void RemoveBlock(Coordinate position)
        {
            blocks.Set(position, null);
        }

        public void PlaceShipComponent(Coordinate position, IShipComponent shipComponent)
        {
            blocks.Get(position).AddShipComponent(shipComponent);
        }

        public void RemoveShipComponent(Coordinate position)
        {
            blocks.Get(position).DeleteShipComponent();
        }

        public void PlacePipe(Coordinate position, DoubleEdgedPipe pipe)
        {
            blocks.Get(position).AddPipe(pipe);
        }

        public void PlacePipe(Coordinate position, ConnectingPipe pipe)
        {
            blocks.Get(position).AddPipe(pipe);
        }

        public void RemovePipe(Coordinate position, DoubleEdgedPipe pipe)
        {
            blocks.Get(position).DeletePipe(pipe);
        }

        public void RemovePipe(Coordinate position, ConnectingPipe pipe)
        {
            blocks.Get(position).DeletePipe(pipe);
        }
    }
}
