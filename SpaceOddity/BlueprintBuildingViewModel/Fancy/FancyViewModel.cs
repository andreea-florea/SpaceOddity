using Game;
using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;

namespace BlueprintBuildingViewModel.Fancy
{
    public class FancyViewModel : IBlueprintObserver
    {
        private IEnumerable<IDetailsViewUpdater> detailsUpdaters;

        public FancyViewModel(IEnumerable<IDetailsViewUpdater> detailsUpdaters)
        {
            this.detailsUpdaters = detailsUpdaters;
        }

        public void BlockCreated(IBlueprint blueprint, Coordinate position)
        {
            UpdateDetails(position);
        }

        public void BlockDeleted(IBlueprint blueprint, Coordinate position)
        {
            UpdateDetails(position);
        }

        private void UpdateDetails(Coordinate position)
        {
            foreach (var detailsUpdater in detailsUpdaters)
            {
                detailsUpdater.UpdateDetails(position);
            }
        }

        public void ShipComponentAdded(IBlueprint blueprint, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ShipComponentDeleted(IBlueprint blueprint, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void DoubleEdgePipeAdded(IBlueprint blueprint, Coordinate position, DoubleEdgedPipe pipe)
        {
            throw new NotImplementedException();
        }

        public void DoubleEdgePipeDeleted(IBlueprint blueprint, Coordinate position, DoubleEdgedPipe pipe)
        {
            throw new NotImplementedException();
        }

        public void ConnectingPipeAdded(IBlueprint blueprint, Coordinate position, ConnectingPipe pipe)
        {
            throw new NotImplementedException();
        }

        public void ConnectingPipeDeleted(IBlueprint blueprint, Coordinate position, ConnectingPipe pipe)
        {
            throw new NotImplementedException();
        }
    }
}
