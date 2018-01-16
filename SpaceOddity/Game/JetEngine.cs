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
        public IBlock Block { get; set; }
        public EdgeType FacingDirection { get; set; }

        public BlueprintShipComponentType Type
        {
            get
            {
                return BlueprintShipComponentType.JetEngine;
            }
        }

        public JetEngine(IBlock block, EdgeType facingDirection)
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
    }
}
