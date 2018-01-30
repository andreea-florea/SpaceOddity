using Game.Interfaces;
using NaturalNumbersMath;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Enums;

namespace Game
{
    public class JetEngine : IShipComponent, IBlockRestrictor
    {
        public IConstBlock Block { get; private set; }
        public EdgeType FacingDirection { get; private set; }

        public BlueprintShipComponentType Type
        {
            get
            {
                return BlueprintShipComponentType.JetEngine;
            }
        }

        public JetEngine(IConstBlock block, EdgeType facingDirection)
        {
            Block = block;
            FacingDirection = facingDirection;
        }

        public bool CanCreateBlock(Coordinate position)
        {
            switch (FacingDirection)
            {
                case EdgeType.DOWN:
                    return !(position.Y > Block.Position.Y && position.X == Block.Position.X);
                case EdgeType.LEFT:
                    return !(position.X < Block.Position.X && position.Y == Block.Position.Y);
                case EdgeType.RIGHT:
                    return !(position.X > Block.Position.X && position.Y == Block.Position.Y);
                case EdgeType.UP:
                    return !(position.Y < Block.Position.Y && position.X == Block.Position.X);
                default: break;
            }

            return true;
        }

        public void AdditionalSetups(IBlueprintBuilder blueprintBuilder)
        {
            blueprintBuilder.AddRestrictor(this);
        }

        public void RemoveAdditionalSetups(IBlueprintBuilder blueprintBuilder)
        {
            blueprintBuilder.RemoveRestrictor(this);
        }

        public bool CanBePlaced(IBlueprint blueprint, Coordinate position)
        {
            switch (FacingDirection)
            {
                case EdgeType.DOWN:
                case EdgeType.UP:
                    return !DoesBlueprintHaveBlocksOnColumn(position, blueprint);
                case EdgeType.LEFT:
                case EdgeType.RIGHT:
                    return !DoesBlueprintHaveBlocksOnLine(position, blueprint);
            }

            return true;
        }

        private bool DoesBlueprintHaveBlocksOnColumn(Coordinate position, IBlueprint blueprint)
        {
            int startLine;
            int endLine;

            switch (FacingDirection)
            {
                case EdgeType.DOWN:
                    startLine = position.Y + 1;
                    endLine = blueprint.Dimensions.Y;
                    break;
                case EdgeType.UP:
                    startLine = 0;
                    endLine = position.Y;
                    break;
                default:
                    startLine = 0;
                    endLine = 0;
                    break;
            }

            for (int i = startLine; i < endLine; i++)
            {
                if (blueprint.HasBlock(new Coordinate(position.X, i)))
                {
                    return true;
                }
            }

            return false;
        }

        private bool DoesBlueprintHaveBlocksOnLine(Coordinate position, IBlueprint blueprint)
        {
            int startColumn;
            int endColumn;

            switch (FacingDirection)
            {
                case EdgeType.LEFT:
                    startColumn = 0;
                    endColumn = position.X;
                    break;
                case EdgeType.RIGHT:
                    startColumn = position.X + 1;
                    endColumn = blueprint.Dimensions.X;
                    break;
                default:
                    startColumn = 0;
                    endColumn = 0;
                    break;
            }

            for (int j = startColumn; j < endColumn; j++)
            {
                if (blueprint.HasBlock(new Coordinate(j, position.Y)))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
