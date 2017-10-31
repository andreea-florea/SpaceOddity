using Game.Interfaces;
using Geometry;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Fancy;

namespace ViewModel
{
    public class BlueprintBuilderFancyViewModel : IBlueprintBuilderObserver
    {
        private IEnumerable<IDetailsViewUpdater> detailsUpdaters;

        public BlueprintBuilderFancyViewModel(IEnumerable<IDetailsViewUpdater> detailsUpdaters)
        {
            this.detailsUpdaters = detailsUpdaters;
        }

        public void BlockCreated(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            UpdateDetails(position);
        }

        public void ErrorBlockNotCreated(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void BlockDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
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

        public void ErrorBlockNotDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ShipComponentAdded(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ErrorShipComponentNotAdded(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ShipComponentDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }

        public void ErrorShipComponentNotDeleted(IBlueprintBuilder blueprintBuilder, Coordinate position)
        {
            throw new NotImplementedException();
        }
    }
}
