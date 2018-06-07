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
        private WorldObjectDictionary<Coordinate, IActivateableWorldObject> blocks;
        private WorldObjectDictionary<Coordinate, IActivateableWorldObject> shipComponents;
        private WorldObjectDictionary<CoordinatePair, IActivateableWorldObject> pipeLinks;
        private WorldObjectDictionary<PipePosition, IWorldObject> doubleEdgedPipes;

        public ObjectTable(
            IActivateableWorldObject[,] tiles,
            WorldObjectDictionary<Coordinate, IActivateableWorldObject> blocks,
            WorldObjectDictionary<Coordinate, IActivateableWorldObject> shipComponents,
            WorldObjectDictionary<CoordinatePair, IActivateableWorldObject> pipeLinks,
            WorldObjectDictionary<PipePosition, IWorldObject> doubleEdgedPipes)
        {
            this.tiles = tiles;
            this.blocks = blocks;
            this.shipComponents = shipComponents;
            this.pipeLinks = pipeLinks;
            this.doubleEdgedPipes = doubleEdgedPipes;
        }

        public IEnumerable<Coordinate> GetCoordinates()
        {
            return tiles.GetCoordinates();
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
            pipeLinks.Remove(edge);
        }

        public void DeleteBlock(Coordinate position)
        {
            blocks.Remove(position);
        }

        public void DeleteShipComponent(Coordinate position)
        {
            shipComponents.Remove(position);
        }

        public void DeletePipe(Coordinate position, EdgeType firstEdge, EdgeType secondEdge)
        {
            doubleEdgedPipes.Remove(new PipePosition(position, firstEdge, secondEdge));
        }
    }
}
