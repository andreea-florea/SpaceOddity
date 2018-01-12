using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.DataStructures;

namespace ViewModel
{
    public interface IBlueprintBuilderObjectTable
    {
        IBuilderWorldObject GetTile(Coordinate position);
        IBuilderWorldObject GetBlock(Coordinate position);
        void SetBlock(Coordinate position, IBuilderWorldObject block);
        IBuilderWorldObject GetShipComponent(Coordinate position);
        void SetShipComponent(Coordinate position, IBuilderWorldObject shipComponent);
        IBuilderWorldObject GetPipeLink(CoordinatePair edge);
        void SetPipeLink(CoordinatePair edge, IBuilderWorldObject pipeLink);
        void DeletePipeLink(CoordinatePair edge);
        void DeleteBlock(Coordinate position);
    }
}
