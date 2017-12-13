using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.DataStructures;

namespace ViewModel
{
    public class BlueprintBuilderTableHighlighter : IBlueprintBuilderTableHighlighter
    {
        private IBlueprintBuilderObjectTable objectTable;
        private IList<IBuilderWorldObject> activatedObjects;

        public BlueprintBuilderTableHighlighter(IBlueprintBuilderObjectTable objectTable)
        {
            this.objectTable = objectTable;
            activatedObjects = new List<IBuilderWorldObject>();
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
