﻿using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlueprintBuildingViewModel.DataStructures;
using ViewModel;

namespace BlueprintBuildingViewModel
{
    public class TableHighlighter : ITableHighlighter
    {
        private IObjectTable objectTable;
        private IList<IActivateableWorldObject> activatedObjects;

        public TableHighlighter(IObjectTable objectTable)
        {
            this.objectTable = objectTable;
            activatedObjects = new List<IActivateableWorldObject>();
        }

        public void ActivatePipeLink(CoordinatePair edge)
        {
            objectTable.GetPipeLink(edge).Activate();
            activatedObjects.Add(objectTable.GetPipeLink(edge));
        }

        public void DeactivateAll()
        {
            foreach (var activatedObject in activatedObjects)
            {
                activatedObject.Deactivate();
            }
            activatedObjects.Clear();
        }
    }
}
