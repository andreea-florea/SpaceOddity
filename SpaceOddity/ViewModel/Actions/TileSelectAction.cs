﻿using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewInterface;
using ViewModel.Interfaces;

namespace ViewModel.Actions
{
    class TileSelectAction : IAction
    {
        private IBlueprintBuilderController controller;
        private Coordinate position;

        public TileSelectAction(IBlueprintBuilderController controller, Coordinate position)
        {
            this.controller = controller;
            this.position = position;
        }

        public void Execute()
        {
            controller.TileSelect(position);
        }
    }
}
