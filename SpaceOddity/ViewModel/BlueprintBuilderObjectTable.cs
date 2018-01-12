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
        private Dictionary<Coordinate, IBuilderWorldObject> blocks;
        private Dictionary<Coordinate, IBuilderWorldObject> shipComponents;
        private Dictionary<CoordinatePair, IBuilderWorldObject> pipeLinks;
        private Dictionary<PipePosition, IWorldObject> doubleEdgedPipes;

        public BlueprintBuilderObjectTable(
            IBuilderWorldObject[,] tiles,
            Dictionary<Coordinate, IBuilderWorldObject> blocks,
            Dictionary<Coordinate, IBuilderWorldObject> shipComponents,
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
            return blocks[position];
        }

        public void SetBlock(Coordinate position, IBuilderWorldObject block)
        {
            blocks.Add(position, block);
        }

        public IBuilderWorldObject GetShipComponent(Coordinate position)
        {
            return shipComponents[position];
        }

        public void SetShipComponent(Coordinate position, IBuilderWorldObject shipComponent)
        {
            shipComponents.Add(position, shipComponent);
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
            blocks[position].Delete();
            blocks.Remove(position);
        }

        public void DeleteShipComponent(Coordinate position)
        {
            shipComponents[position].Delete();
            shipComponents.Remove(position);
        }

        public void DeletePipe(Coordinate position, EdgeType firstEdge, EdgeType secondEdge)
        {
            var key = new PipePosition(position, firstEdge, secondEdge);
            doubleEdgedPipes[key].Delete();
            doubleEdgedPipes.Remove(key);
        }
    }
}
