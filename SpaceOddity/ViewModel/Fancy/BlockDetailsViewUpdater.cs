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
    public class BlockDetailsViewUpdater : IDetailsViewUpdater
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
            detailObject.Position = GetTile(position).Position;
            detailObject.Scale = GetTile(position).Scale;
            detailObject.Rotation = new Vector2(facing.X, facing.Y);
            return detailObject;
        }

        private void ReplaceOldDetail(IWorldObject newDetail, Coordinate position)
        {
            if (GetDetail(position) != null)
            {
                GetDetail(position).Delete();
            }
            SetDetail(newDetail, position);
        }

        private IWorldObject GetDetail(Coordinate position)
        {
            return details[position.Y, position.X];
        }

        private void SetDetail(IWorldObject detail, Coordinate position)
        {
            details[position.Y, position.X] = detail;
        }

        private IWorldObject GetTile(Coordinate position)
        {
            return tiles[position.Y, position.X];
        }
    }
}
