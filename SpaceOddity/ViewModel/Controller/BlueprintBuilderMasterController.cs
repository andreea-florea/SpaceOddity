using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.DataStructures;

namespace ViewModel.Controller
{
    public class BlueprintBuilderMasterController : IBlueprintBuilderController
    {
        public IBlueprintBuilderController BaseController { get; set; }
        public IBlueprintBuilderController CurrentController { get; set; }

        private IBlueprintBuilderTableHighlighter tableHighlighter;

        public BlueprintBuilderMasterController(IBlueprintBuilderController baseController,
            IBlueprintBuilderController currentController,
            IBlueprintBuilderTableHighlighter tableHighlighter)
        {
            BaseController = baseController;
            CurrentController = currentController;
            this.tableHighlighter = tableHighlighter;
        }

        public void TileSelect(Coordinate position)
        {
            CurrentController.TileSelect(position);
        }

        public void BlockSelect(Coordinate position)
        {
            CurrentController.BlockSelect(position);
        }

        public void BlockCancel(Coordinate position)
        {
            CurrentController.BlockCancel(position);
        }

        public void ShipComponentSelect(Coordinate position)
        {
            CurrentController.ShipComponentSelect(position);
        }

        public void ShipComponentCancel(Coordinate position)
        {
            CurrentController.ShipComponentCancel(position);
        }

        public void PipeLinkSelect(CoordinatePair edge)
        {
            CurrentController.PipeLinkSelect(edge);
        }

        public void Reset()
        {
            CurrentController = BaseController;
            tableHighlighter.DeactivateAll();
        }

        public void ActivatePipeLink(CoordinatePair edge)
        {
            tableHighlighter.ActivatePipeLink(edge);
        }
    }
}
