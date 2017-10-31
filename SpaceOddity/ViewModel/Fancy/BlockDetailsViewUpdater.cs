using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Interfaces;

namespace ViewModel.Fancy
{
    internal class BlockDetailsViewUpdater : IDetailsViewUpdater
    {
        private IBlueprintBuilder blueprintBuilder;
        private IWorldObject[,] tiles;
        private IWorldObject[,] details;
        private IFacingContextWorldObjectFactory detailFactory;
        private IBlueprintBuilderController controller;
        private IEnumerable<FacingPosition> detailUpdates;

        public BlockDetailsViewUpdater(IBlueprintBuilder blueprintBuilder,
            IWorldObject[,] tiles,
            IWorldObject[,] details,
            IFacingContextWorldObjectFactory detailFactory,
            IBlueprintBuilderController controller,
            IEnumerable<FacingPosition> detailUpdates)
        {
            this.blueprintBuilder = blueprintBuilder;
            this.tiles = tiles;
            this.details = details;
            this.detailFactory = detailFactory;
            this.controller = controller;
            this.detailUpdates = detailUpdates;
        }

        public void UpdateDetails(Coordinate updatePosition)
        {
            foreach (var detailUpdate in detailUpdates)
            {
                var position = updatePosition + detailUpdate.RelativePosition;
                DeleteOldDetail(position);
                if (blueprintBuilder.HasBlock(position))
                {
                    CreateDetailObject(position, detailUpdate.Forward);
                }
            }
        }

        private void CreateDetailObject(Coordinate position, Coordinate facing)
        {
            var detailObject = detailFactory.CreateObject(position, facing);
            detailObject.Position = tiles.Get(position).Position;
            detailObject.Scale = tiles.Get(position).Scale;
            detailObject.Rotation = new Vector2(facing.X, facing.Y);
            controller.AssignBlockControl(blueprintBuilder, detailObject, position);
            details.Set(position, detailObject);
        }

        private void DeleteOldDetail(Coordinate position)
        {
            if (details.IsWithinBounds(position) && details.Get(position) != null)
            {
                details.Get(position).Delete();
            }
        }
    }
}
