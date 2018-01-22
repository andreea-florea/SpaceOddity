using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueprintBuildingViewModel.DataStructures;

namespace BlueprintBuildingViewModel.Controller
{
    public class ControllerFactory
    {
        public IController CreateController(IBlueprintBuilder blueprintBuilder,
            ITableHighlighter tableHighlighter)
        {
            var controller = new MasterController(null, null, tableHighlighter);
            var pipeBuildController = new PipeBuildController(controller, blueprintBuilder, new CoordinatePair());
            var basicController = new BasicController(controller, pipeBuildController, blueprintBuilder);
            controller.BaseController = basicController;
            controller.Reset();
            return controller;
        }
    }
}
