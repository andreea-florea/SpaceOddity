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

        public void BlockCreated(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            var position = new Coordinate(x, y);
            foreach (var detailsUpdater in detailsUpdaters)
            {
                detailsUpdater.UpdateDetails(position);
            }
        }

        public void ErrorBlockNotCreated(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void BlockDeleted(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void ErrorBlockNotDeleted(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void ShipComponentAdded(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void ErrorShipComponentNotAdded(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void ShipComponentDeleted(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }

        public void ErrorShipComponentNotDeleted(IBlueprintBuilder blueprintBuilder, int y, int x)
        {
            throw new NotImplementedException();
        }
    }
}
