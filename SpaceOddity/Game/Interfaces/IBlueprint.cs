using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlueprint
    {
        Coordinate Dimensions { get; }

        void AttachObserver(IBlueprintObserver observer);

        IConstBlock GetBlock(Coordinate position);
        bool HasBlock(Coordinate position);
        void PlaceBlock(Coordinate position, IBlock block);
        void RemoveBlock(Coordinate position);
        void PlaceShipComponent(Coordinate position, IShipComponent shipComponent);
        void RemoveShipComponent(Coordinate position);
        void PlacePipe(Coordinate position, DoubleEdgedPipe pipe);
        void PlacePipe(Coordinate position, ConnectingPipe pipe);
        void RemovePipe(Coordinate position, DoubleEdgedPipe pipe);
        void RemovePipe(Coordinate position, ConnectingPipe pipe);
    }
}
