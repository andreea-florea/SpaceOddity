using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Game.Interfaces;
using NaturalNumbersMath;

namespace Game
{
    public class BlueprintBuilder : IBlueprintBuilder
    {
        private IBlock[,] blueprint;
        private IBlockFactory blockFactory;
        private IShipComponentFactory shipComponentFactory;

        public Coordinate Dimensions
        {
            get 
            {
                return new Coordinate(blueprint.GetLength(1), blueprint.GetLength(0));
            }
        }

        public BlueprintBuilder(IBlock[,] blueprint, IBlockFactory blockFactory, IShipComponentFactory shipComponentFactory)
        {
            this.blueprint = blueprint;
            this.blockFactory = blockFactory;
            this.shipComponentFactory = shipComponentFactory;
        }

        public BlueprintBuilder(Coordinate dimensions)
        {
            //TODO: check negatives?
            blueprint = new IBlock[dimensions.X, dimensions.Y];
            blockFactory = new BlockFactory(5);
            shipComponentFactory = new BatteryFactory();
        }

        public IBlock GetBlock(Coordinate position)
        {
            return blueprint.Get(position);
        }

        public bool HasBlock(Coordinate position)
        {
            return (blueprint.IsWithinBounds(position) && GetBlock(position) != null);
        }

        public bool CreateBlock(Coordinate position)
        {
            //TODO: check that position in within bounds?
            if (GetBlock(position) == null)
            {
                blueprint.Set(position, blockFactory.CreateBlock());
                return true;
            }
            return false;
        }

        public bool DeleteBlock(Coordinate position)
        {
            if (GetBlock(position) == null)
            {
                return false;
            }

            blueprint.Set(position, null);
            return true;
        }

        public bool AddShipComponent(Coordinate position)
        {
            var component = shipComponentFactory.CreateComponent();
            if (GetBlock(position) != null && !(GetBlock(position).HasShipComponent()))
            {
                GetBlock(position).AddShipComponent(component);
                return true;
            }

            return false;
        }

        public bool DeleteShipComponent(Coordinate position)
        {
            if (GetBlock(position) != null && GetBlock(position).HasShipComponent())
            {
                GetBlock(position).DeleteShipComponent();
                return true;
            }

            return false;
        }

        public bool AddDoubleEdgedPipe(Coordinate position, EdgeType firstEdge, EdgeType secondEdge)
        {
            var pipe = new DoubleEdgedPipe()
            {
                FirstEdge = firstEdge,
                SecondEdge = secondEdge
            };

            var block = GetBlock(position);

            if (block != null)
            {
                if (!(block.HasShipComponent()))
                {
                    var intersectingPipe = HasIntersectingPipes(block, pipe);
                    if (intersectingPipe != null)
                    {
                        TransformDoubleEdgedPipeIntoConnectingPipe(block, intersectingPipe);
                        TransformDoubleEdgedPipeIntoConnectingPipe(block, pipe);
                        block.AddShipComponent(new EmptyShipComponent());
                        block.PipesWithBothEdges.Remove(intersectingPipe);
                    }
                    else
                    {
                        block.PipesWithBothEdges.Add(pipe);
                    }
                    return true;
                }
            }

            return false;
        }

        private DoubleEdgedPipe HasIntersectingPipes(IBlock block, DoubleEdgedPipe pipeToCheck)
        {
            foreach (DoubleEdgedPipe pipe in block.PipesWithBothEdges)
            {
                if (DoPipesIntersect(pipe, pipeToCheck))
                {
                    return pipe;
                }
            }
            return null;
        }

        private bool DoPipesIntersect(DoubleEdgedPipe pipe1, DoubleEdgedPipe pipe2)
        {
            if ((pipe1.FirstEdge == EdgeType.UP && pipe1.SecondEdge == EdgeType.DOWN) || (pipe1.FirstEdge == EdgeType.DOWN && pipe1.SecondEdge == EdgeType.UP))
            {
                if ((pipe2.FirstEdge == EdgeType.LEFT && pipe2.SecondEdge == EdgeType.RIGHT) || (pipe2.FirstEdge == EdgeType.RIGHT && pipe2.SecondEdge == EdgeType.LEFT))
                {
                    return true;
                }
            }

            if ((pipe1.FirstEdge == EdgeType.LEFT && pipe1.SecondEdge == EdgeType.RIGHT) || (pipe1.FirstEdge == EdgeType.RIGHT && pipe1.SecondEdge == EdgeType.LEFT))
            {
                if ((pipe2.FirstEdge == EdgeType.UP && pipe2.SecondEdge == EdgeType.DOWN) || (pipe2.FirstEdge == EdgeType.DOWN && pipe2.SecondEdge == EdgeType.UP))
                {
                    return true;
                }
            }

            return false;
        }

        private void TransformDoubleEdgedPipeIntoConnectingPipe(IBlock block, DoubleEdgedPipe pipe)
        {
            block.PipesWithOneEdge.Add(new ConnectingPipe() { Edge = pipe.FirstEdge });
            block.PipesWithOneEdge.Add(new ConnectingPipe() { Edge = pipe.SecondEdge }); 
        }
    }
}
