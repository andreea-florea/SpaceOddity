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

        public BlueprintBuilderMasterController(IBlueprintBuilderController baseController,
            IBlueprintBuilderController currentController)
        {
            BaseController = baseController;
            CurrentController = currentController;
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

        public void PipeLinkSelect(CoordinatePair edge)
        {
            CurrentController.PipeLinkSelect(edge);
        }

        public void Reset()
        {
            CurrentController = BaseController;
        }
    }
}
