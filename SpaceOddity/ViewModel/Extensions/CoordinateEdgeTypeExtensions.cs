using Game.Enums;
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
        private static Dictionary<EdgeType, Coordinate> coordinates;

        static CoordinateEdgeTypeExtensions()
        {
            edges = new Dictionary<Coordinate, EdgeType>();
            coordinates = new Dictionary<EdgeType, Coordinate>();
            AddPair(Coordinates.Up, EdgeType.UP);
            AddPair(Coordinates.Down, EdgeType.DOWN);
            AddPair(Coordinates.Right, EdgeType.RIGHT);
            AddPair(Coordinates.Left, EdgeType.LEFT);
        }

        private static void AddPair(Coordinate coordinate, EdgeType edge)
        {
            edges.Add(coordinate, edge);
            coordinates.Add(edge, coordinate);
        }

        public static EdgeType ToEdgeType(this Coordinate coordinate)
        {
            return edges[coordinate];
        }

        public static Coordinate ToCoordinate(this EdgeType edgeType)
        {
            return coordinates[edgeType];
        }
    }
}
