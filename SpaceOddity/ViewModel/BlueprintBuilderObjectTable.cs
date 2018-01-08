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
        private Dictionary<CoordinatePair, IBuilderWorldObject> pipeLinks;
        private Dictionary<PipePosition, IWorldObject> doubleEdgedPipes;

        public BlueprintBuilderObjectTable(
            IBuilderWorldObject[,] tiles,
            IBuilderWorldObject[,] blocks,
            IBuilderWorldObject[,] shipComponents,
            Dictionary<CoordinatePair, IBuilderWorldObject> pipeLinks,
            Dictionary<PipePosition, IWorldObject> doubleEdgedPipes)
        {
            this.tiles = tiles;
            this.blocks = blocks;
            this.shipComponents = shipComponents;
            this.pipeLinks = pipeLinks;
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
            doubleEdgedPipes.Add(new PipePosition(position, firstEdge, secondEdge), pipeObject);
        }

        public IWorldObject GetPipe(Coordinate position, EdgeType firstEdge, EdgeType secondEdge)
        {
            return doubleEdgedPipes[new PipePosition(position, firstEdge, secondEdge)];
        }

        public IBuilderWorldObject GetPipeLink(CoordinatePair edge)
        {
            return pipeLinks[edge];
        }

        public void SetPipeLink(CoordinatePair edge, IBuilderWorldObject pipeLink)
        {
            pipeLinks[edge] = pipeLink;
        }

        public bool HasPipeLink(CoordinatePair edge)
        {
            return pipeLinks.ContainsKey(edge);
        }

        public void DeletePipeLink(CoordinatePair edge)
        {
            pipeLinks[edge].Delete();
            pipeLinks.Remove(edge);
        }

        public void DeleteBlock(Coordinate position)
        {
            DeleteObject(blocks, position);
        }

        public void DeleteShipComponent(Coordinate position)
        {
            DeleteObject(shipComponents, position);
        }

        public void DeletePipe(Coordinate position, EdgeType firstEdge, EdgeType secondEdge)
        {
            var key = new PipePosition(position, firstEdge, secondEdge);
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
