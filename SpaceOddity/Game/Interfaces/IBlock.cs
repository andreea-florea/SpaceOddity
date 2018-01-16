using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Interfaces
{
    public interface IBlock : IConstBlock
    {
        void AddShipComponent(IShipComponent component);
        void DeleteShipComponent();
        void AddPipe(DoubleEdgedPipe pipe);
        void AddPipe(ConnectingPipe pipe);
        void DeletePipe(DoubleEdgedPipe pipe);
        void DeletePipe(ConnectingPipe pipe);
    }
}
