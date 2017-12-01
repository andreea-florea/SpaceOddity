using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Controller;

namespace ViewModel.Fancy.Iternal
{
    internal class BlockDetailsViewUpdater : IDetailsViewUpdater
    {
        private IBlueprintBuilder blueprintBuilder;
        private IWorldObject[,] tiles;
        private IWorldObject[,] details;
        private IFacingContextWorldObjectFactory detailFactory;
        private IBlueprintBuilderControlAssigner controller;
        private IEnumerable<FacingPosition> relativeDetailUpdates;

        public BlockDetailsViewUpdater(IBlueprintBuilder blueprintBuilder,
            IWorldObject[,] tiles,
            IWorldObject[,] details,
            IFacingContextWorldObjectFactory detailFactory,
            IBlueprintBuilderControlAssigner controller,
            IEnumerable<FacingPosition> relativeDetailUpdates)
        {
            this.blueprintBuilder = blueprintBuilder;
            this.tiles = tiles;
            this.details = details;
            this.detailFactory = detailFactory;
            this.controller = controller;
            this.relativeDetailUpdates = relativeDetailUpdates;
        }

        public void UpdateDetails(Coordinate updatePosition)
        {
            foreach (var relativeDetailUpdate in relativeDetailUpdates)
            {
                var detailUpdate = relativeDetailUpdate.GetGlobal(updatePosition);
                DeleteOldDetail(detailUpdate.Position);
                if (blueprintBuilder.HasBlock(detailUpdate.Position))
                {
                    CreateDetailObject(detailUpdate);
                }
            }
        }

        private void CreateDetailObject(FacingPosition detailUpdate)
        {
            var detailObject = detailFactory.CreateObject(detailUpdate);
            detailObject.Position = tiles.Get(detailUpdate.Position).Position;
            detailObject.Scale = tiles.Get(detailUpdate.Position).Scale;
            detailObject.Rotation = detailUpdate.Forward.ToVector2();
            controller.AssignBlockControl(detailObject, detailUpdate.Position);
            details.Set(detailUpdate.Position, detailObject);
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
