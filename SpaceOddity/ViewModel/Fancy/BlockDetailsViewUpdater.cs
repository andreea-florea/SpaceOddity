using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace ViewModel.Fancy
{
    internal class BlockDetailsViewUpdater : IDetailsViewUpdater
    {
        private IBlueprintBuilder blueprintBuilder;
        private IWorldObject[,] tiles;
        private IWorldObject[,] details;
        private IFacingContextWorldObjectFactory detailFactory;
        private IEnumerable<FacingPosition> detailUpdates;

        public BlockDetailsViewUpdater(IBlueprintBuilder blueprintBuilder,
            IWorldObject[,] tiles,
            IWorldObject[,] details,
            IFacingContextWorldObjectFactory detailFactory, 
            IEnumerable<FacingPosition> detailUpdates)
        {
            this.blueprintBuilder = blueprintBuilder;
            this.tiles = tiles;
            this.details = details;
            this.detailFactory = detailFactory;
            this.detailUpdates = detailUpdates;
        }

        public void UpdateDetails(Coordinate updatePosition)
        {
            foreach (var detailUpdate in detailUpdates)
            {
                var position = updatePosition + detailUpdate.RelativePosition;
                if (HasBlock(position))
                {
                    var newDetail = CreateDetailObject(position, detailUpdate.Forward);
                    ReplaceOldDetail(newDetail, position);
                }
            }
        }

        private bool HasBlock(Coordinate position)
        {
            return blueprintBuilder.GetBlock(position) != null;
        }

        private IWorldObject CreateDetailObject(Coordinate position, Coordinate facing)
        {
            var detailObject = detailFactory.CreateObject(position, facing);
            detailObject.Position = tiles.Get(position).Position;
            detailObject.Scale = tiles.Get(position).Scale;
            detailObject.Rotation = new Vector2(facing.X, facing.Y);
            return detailObject;
        }

        private void ReplaceOldDetail(IWorldObject newDetail, Coordinate position)
        {
            if (details.Get(position) != null)
            {
                details.Get(position).Delete();
            }
            details.Set(position, newDetail);
        }
    }
}
