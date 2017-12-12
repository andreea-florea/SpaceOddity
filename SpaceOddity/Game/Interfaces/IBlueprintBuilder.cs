using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlueprintBuilder
    {
        Coordinate Dimensions { get; }
        IConstBlock GetBlock(Coordinate position);
        bool HasBlock(Coordinate position);
        bool CreateBlock(Coordinate position);
        bool DeleteBlock(Coordinate position);
        bool AddShipComponent(Coordinate position);
        bool DeleteShipComponent(Coordinate position);
        bool AddDoubleEdgedPipe(Coordinate position, EdgeType firstEdge, EdgeType secondEdge);
        bool AddConnectingPipe(Coordinate position, EdgeType edge);
        bool DeleteDoubleEdgedPipe(Coordinate position, DoubleEdgedPipe pipe);
        bool DeleteConnectingPipe(Coordinate position, ConnectingPipe pipe);
    }
}
