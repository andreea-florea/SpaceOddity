using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Interfaces;
using Algorithms;
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
                return new Coordinate(blueprint.Width(), blueprint.Height());
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
            if (!HasBlock(position))
            {
                blueprint.Set(position, blockFactory.CreateBlock());
                return true;
            }
            return false;
        }

        public bool DeleteBlock(Coordinate position)
        {
            if (!HasBlock(position))
            {
                return false;
            }

            blueprint.Set(position, null);
            return true;
        }

        public bool AddShipComponent(Coordinate position)
        {
            var block = GetBlock(position);
            if (HasBlock(position) && !(block.HasShipComponent()))
            {
                var component = shipComponentFactory.CreateComponent();
                block.AddShipComponent(component);

                foreach(var pipe in block.PipesWithBothEdges)
                {
                    TransformDoubleEdgedPipeIntoConnectingPipe(block, pipe);
                }

                block.PipesWithBothEdges.Clear();
                
                return true;
            }

            return false;
        }

        public bool DeleteShipComponent(Coordinate position)
        {
            if (HasBlock(position) && GetBlock(position).HasShipComponent())
            {
                var block = GetBlock(position);
                block.DeleteShipComponent();

                var pipes = block.PipesWithOneEdge.Select(pipe => pipe.Edge).ToList();
                block.PipesWithOneEdge.Clear();
                pipes.ForeachPair((e1, e2) => AddDoubleEdgedPipe(position, e1, e2));

                //switch (block.PipesWithOneEdge.Count)
                //{
                //    case 1:
                //        block.PipesWithOneEdge.Clear();
                //        break;
                //    case 2:
                //        block.PipesWithBothEdges.Add(new DoubleEdgedPipe(
                //            block.PipesWithOneEdge[0].Edge, block.PipesWithOneEdge[1].Edge));
                //        block.PipesWithOneEdge.Clear();
                //        break;
                //    case 3:
                //        block.PipesWithBothEdges.Add(new DoubleEdgedPipe(
                //            block.PipesWithOneEdge[0].Edge, block.PipesWithOneEdge[1].Edge));
                //        block.PipesWithBothEdges.Add(new DoubleEdgedPipe(
                //            block.PipesWithOneEdge[1].Edge, block.PipesWithOneEdge[2].Edge));
                //        block.PipesWithBothEdges.Add(new DoubleEdgedPipe(
                //            block.PipesWithOneEdge[2].Edge, block.PipesWithOneEdge[0].Edge));
                //        block.PipesWithOneEdge.Clear();
                //        break;
                //    case 4:
                //        block.AddShipComponent(new EmptyShipComponent());
                //        break;
                //    default: break;
                //}

                return true;
            }

            return false;
        }

        public bool AddDoubleEdgedPipe(Coordinate position, EdgeType firstEdge, EdgeType secondEdge)
        {
            var pipe = new DoubleEdgedPipe(firstEdge, secondEdge);
            var block = GetBlock(position);
            
            if (block != null)
            {
                if (!CheckIfDoubleEdgeAlreadyExists(pipe, block))
                {
                    if (!(block.HasShipComponent()))
                    {
                        var intersectingPipe = HasIntersectingPipes(block, pipe);
                        if (intersectingPipe != null)
                        {
                            TransformDoubleEdgedPipeIntoConnectingPipe(block, intersectingPipe);
                            TransformDoubleEdgedPipeIntoConnectingPipe(block, pipe);
                            block.AddShipComponent(new EmptyShipComponent());
                            block.PipesWithBothEdges.Clear();
                        }
                        else
                        {
                            block.PipesWithBothEdges.Add(pipe);
                        }
                        return true;
                    }
                    else
                    {
                        TransformDoubleEdgedPipeIntoConnectingPipe(block, pipe);
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        private DoubleEdgedPipe HasIntersectingPipes(IBlock block, DoubleEdgedPipe pipeToCheck)
        {
            return block.PipesWithBothEdges.FirstOrDefault(pipe => DoPipesIntersect(pipe, pipeToCheck));
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
            AddConnectingPipe(block, new ConnectingPipe(pipe.FirstEdge));
            AddConnectingPipe(block, new ConnectingPipe(pipe.SecondEdge));
        }

        private void AddConnectingPipe(IBlock block, ConnectingPipe pipe)
        {
            if (!CheckIfConnectingPipeAlreadyExists(pipe, block))
            {
                block.PipesWithOneEdge.Add(pipe);
            }
        }

        private bool CheckIfDoubleEdgeAlreadyExists(DoubleEdgedPipe pipe, IBlock block)
        {
            return block.PipesWithBothEdges.Any(p => p.IsEqualTo(pipe));
        }

        private bool CheckIfConnectingPipeAlreadyExists(ConnectingPipe pipe, IBlock block)
        {
            return block.PipesWithOneEdge.Any(p => p.IsEqualTo(pipe));
        }
    }
}
