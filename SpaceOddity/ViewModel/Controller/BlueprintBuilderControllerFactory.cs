using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.DataStructures;

namespace ViewModel.Controller
{
    public class BlueprintBuilderControllerFactory
    {
        public IBlueprintBuilderController CreateController(IBlueprintBuilder blueprintBuilder)
        {
            var controller = new BlueprintBuilderMasterController(null, null);
            var pipeBuildController = new BlueprintBuilderPipeBuildController(controller, blueprintBuilder, new CoordinatePair());
            var basicController = new BlueprintBuilderBasicController(controller, pipeBuildController, blueprintBuilder);
            controller.BaseController = basicController;
            controller.Reset();
            return controller;
        }
    }
}
