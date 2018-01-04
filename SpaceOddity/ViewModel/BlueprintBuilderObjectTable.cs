using Game;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.DataStructures;
using Game.Enums;

namespace ViewModel
{
    public class BlueprintBuilderObjectTable : IBlueprintBuilderObjectTable
    {
        private IBuilderWorldObject[,] tiles;
        private IBuilderWorldObject[,] blocks;
        private IBuilderWorldObject[,] shipComponents;
        private IBuilderWorldObject[,] horizontalPipeLinks;
        private IBuilderWorldObject[,] verticalPipeLinks;
        private Dictionary<DoubleEdgedPipePosition, IWorldObject> doubleEdgedPipes;

        public BlueprintBuilderObjectTable(
            IBuilderWorldObject[,] tiles,
            IBuilderWorldObject[,] blocks,
            IBuilderWorldObject[,] shipComponents,
            IBuilderWorldObject[,] horizontalPipeLinks,
            IBuilderWorldObject[,] verticalPipeLinks,
            Dictionary<DoubleEdgedPipePosition, IWorldObject> doubleEdgedPipes)
        {
            this.tiles = tiles;
            this.blocks = blocks;
            this.shipComponents = shipComponents;
            this.horizontalPipeLinks = horizontalPipeLinks;
            this.verticalPipeLinks = verticalPipeLinks;
            this.doubleEdgedPipes = doubleEdgedPipes;
        }

        public IBuilderWorldObject GetTile(Coordinate position)
        {
            return tiles.Get(position);
        }

        public IBuilderWorldObject GetBlock(Coordinate position)
        {
            return blocks.Get(position);
        }

        public void SetBlock(Coordinate position, IBuilderWorldObject block)
        {
            blocks.Set(position, block);
        }

        public IBuilderWorldObject GetShipComponent(Coordinate position)
        {
            return shipComponents.Get(position);
        }

        public void SetShipComponent(Coordinate position, IWorldObject shipComponent)
        {
            shipComponents.Set(position, shipComponent);
        }

        public void SetPipe(Coordinate position, EdgeType firstEdge, EdgeType secondEdge, IWorldObject pipeObject)
        {
            doubleEdgedPipes.Add(new DoubleEdgedPipePosition(position, firstEdge, secondEdge), pipeObject);
        }

        public IWorldObject GetPipe(Coordinate position, EdgeType firstEdge, EdgeType secondEdge)
        {
            return doubleEdgedPipes[new DoubleEdgedPipePosition(position, firstEdge, secondEdge)];
        }

        public IBuilderWorldObject GetPipeLink(CoordinatePair edge)
        {
            var position = MinPosition(edge);
            return GetPipeLinkArray(edge).Get(position);
        }

        public void SetPipeLink(CoordinatePair edge, IWorldObject pipeLink)
        {
            var position = MinPosition(edge);
            GetPipeLinkArray(edge).Set(position, pipeLink);
        }

        public void DeletePipeLink(CoordinatePair edge)
        {
            var position = MinPosition(edge);
            DeleteObject(GetPipeLinkArray(edge), position);
        }

        private Coordinate MinPosition(CoordinatePair pair)
        {
            return new Coordinate(
                Math.Min(pair.First.X, pair.Second.X),
                Math.Min(pair.First.Y, pair.Second.Y));
        }

        private IBuilderWorldObject[,] GetPipeLinkArray(CoordinatePair edge)
        {
            if ((edge.First - edge.Second).X == 0)
            {
                return verticalPipeLinks;
            }
            return horizontalPipeLinks;
        }

        public void DeleteBlock(Coordinate position)
        {
            DeleteObject(blocks, position);
        }

        public void DeletePipe(Coordinate position, EdgeType firstEdge, EdgeType secondEdge)
        {
            var key = new DoubleEdgedPipePosition(position, firstEdge, secondEdge);
            doubleEdgedPipes[key].Delete();
            doubleEdgedPipes.Remove(key);
        }

        private void DeleteObject(IWorldObject[,] array, Coordinate position)
        {
            if (array.Get(position) != null)
            {
                array.Get(position).Delete();
                array.Set(position, null);
            }
        }
    }
}
