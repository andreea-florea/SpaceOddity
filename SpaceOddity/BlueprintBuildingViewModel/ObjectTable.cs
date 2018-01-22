using Game;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueprintBuildingViewModel.DataStructures;
using Game.Enums;
using ViewModel;

namespace BlueprintBuildingViewModel
{
    public class ObjectTable : IObjectTable
    {
        private IActivateableWorldObject[,] tiles;
        private Dictionary<Coordinate, IActivateableWorldObject> blocks;
        private Dictionary<Coordinate, IActivateableWorldObject> shipComponents;
        private Dictionary<CoordinatePair, IActivateableWorldObject> pipeLinks;
        private Dictionary<PipePosition, IWorldObject> doubleEdgedPipes;

        public ObjectTable(
            IActivateableWorldObject[,] tiles,
            Dictionary<Coordinate, IActivateableWorldObject> blocks,
            Dictionary<Coordinate, IActivateableWorldObject> shipComponents,
            Dictionary<CoordinatePair, IActivateableWorldObject> pipeLinks,
            Dictionary<PipePosition, IWorldObject> doubleEdgedPipes)
        {
            this.tiles = tiles;
            this.blocks = blocks;
            this.shipComponents = shipComponents;
            this.pipeLinks = pipeLinks;
            this.doubleEdgedPipes = doubleEdgedPipes;
        }

        public IActivateableWorldObject GetTile(Coordinate position)
        {
            return tiles.Get(position);
        }

        public IActivateableWorldObject GetBlock(Coordinate position)
        {
            return blocks[position];
        }

        public void SetBlock(Coordinate position, IActivateableWorldObject block)
        {
            blocks.Add(position, block);
        }

        public IActivateableWorldObject GetShipComponent(Coordinate position)
        {
            return shipComponents[position];
        }

        public void SetShipComponent(Coordinate position, IActivateableWorldObject shipComponent)
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

        public IActivateableWorldObject GetPipeLink(CoordinatePair edge)
        {
            return pipeLinks[edge];
        }

        public void SetPipeLink(CoordinatePair edge, IActivateableWorldObject pipeLink)
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
