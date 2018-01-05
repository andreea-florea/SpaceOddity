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
                    if (position.Y > Block.Position.Y && position.X == Block.Position.X) return false;
                    break;
                case EdgeType.LEFT:
                    if (position.X < Block.Position.X && position.Y == Block.Position.Y) return false;
                    break;
                case EdgeType.RIGHT:
                    if (position.X > Block.Position.X && position.Y == Block.Position.Y) return false;
                    break;
                case EdgeType.UP:
                    if (position.Y < Block.Position.Y && position.X == Block.Position.X) return false;
                    break;
                default: break;
            }

            return true;
        }
    }
}
