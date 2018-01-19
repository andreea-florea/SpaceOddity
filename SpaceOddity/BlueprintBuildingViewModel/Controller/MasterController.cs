using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueprintBuildingViewModel.DataStructures;

namespace BlueprintBuildingViewModel.Controller
{
    public class MasterController : IController
    {
        public IController BaseController { get; set; }
        public IController CurrentController { get; set; }

        private ITableHighlighter tableHighlighter;

        public MasterController(IController baseController,
            IController currentController,
            ITableHighlighter tableHighlighter)
        {
            BaseController = baseController;
            CurrentController = currentController;
            this.tableHighlighter = tableHighlighter;
        }

        public void SelectTile(Coordinate position)
        {
            CurrentController.SelectTile(position);
        }

        public void SelectBlock(Coordinate position)
        {
            CurrentController.SelectBlock(position);
        }

        public void CancelBlock(Coordinate position)
        {
            CurrentController.CancelBlock(position);
        }

        public void SelectShipComponent(Coordinate position)
        {
            CurrentController.SelectShipComponent(position);
        }

        public void CancelShipComponent(Coordinate position)
        {
            CurrentController.CancelShipComponent(position);
        }

        public void SelectPipeLink(CoordinatePair edge)
        {
            CurrentController.SelectPipeLink(edge);
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
