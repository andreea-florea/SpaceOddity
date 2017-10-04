using Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel.Interfaces
{
    public interface IRectangle
    {
        Vector2 TopLeftCorner { get; }
        Vector2 BottomRightCorner { get; }
        IRectangle[,] Split(int width, int height);
    }
}
