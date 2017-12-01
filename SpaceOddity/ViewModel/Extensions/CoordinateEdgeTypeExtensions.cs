using Game;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Extensions
{
    public static class CoordinateEdgeTypeExtensions
    {
        private static Dictionary<Coordinate, EdgeType> edges;

        static CoordinateEdgeTypeExtensions()
        {
            edges = new Dictionary<Coordinate, EdgeType>();
            edges.Add(Coordinates.Up, EdgeType.UP);
            edges.Add(Coordinates.Down, EdgeType.DOWN);
            edges.Add(Coordinates.Right, EdgeType.RIGHT);
            edges.Add(Coordinates.Left, EdgeType.LEFT);
        }
        public static EdgeType GetEdgeType(this Coordinate coordinate)
        {
            return edges[coordinate];
        }
    }
}
