using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Interfaces;
using Algorithms;
using NaturalNumbersMath;
using Game.Enums;

namespace Game
{
    public class BlueprintBuilder : IBlueprintBuilder
    {
        private IBlueprint blueprint;
        private IBlockFactory blockFactory;
        private IShipComponentFactory shipComponentFactory;
        private IShipComponentFactory emptyShipComponentFactory;
        private List<IBlockRestrictor> blockRestrictors;

        public Coordinate Dimensions
        {
            get
            {
                return blueprint.Dimensions;
            }
        }

        public BlueprintBuilder(IBlueprint blueprint, IBlockFactory blockFactory, IShipComponentFactory shipComponentFactory, IShipComponentFactory emptyShipComponentFactory)
        {
            this.blueprint = blueprint;
            this.blockFactory = blockFactory;
            this.shipComponentFactory = shipComponentFactory;
            this.emptyShipComponentFactory = emptyShipComponentFactory;
            blockRestrictors = new List<IBlockRestrictor>();
        }

        public BlueprintBuilder(Coordinate dimensions)
        {
            var blocks = new IBlock[dimensions.X, dimensions.Y];
            blueprint = new Blueprint(blocks);
            blockFactory = new BlockFactory(5);
            shipComponentFactory = new BatteryFactory();
            blockRestrictors = new List<IBlockRestrictor>();
        }

        public void AttachObserver(IBlueprintObserver observer)
        {
            blueprint.AttachObserver(observer);
        }

        public IConstBlock GetBlock(Coordinate position)
        {
            return blueprint.GetBlock(position);
        }

        public bool HasBlock(Coordinate position)
        {
            return blueprint.HasBlock(position);
        }

        public bool CreateBlock(Coordinate position)
        {
            if (!HasBlock(position))
            {
                blueprint.PlaceBlock(position, blockFactory.CreateBlock());
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

            var block = GetBlock(position);
            DeleteShipComponent(position);
            ClearPipes(position, block.PipesWithBothEdges);
            blueprint.RemoveBlock(position);

            return true;
        }

        public bool AddShipComponent(Coordinate position)
        {
            var block = GetBlock(position);
            if (HasBlock(position) && !(block.HasShipComponent()))
            {
                var component = shipComponentFactory.Create();                
                blueprint.PlaceShipComponent(position, component);
                component.AdditionalSetups(this);

                foreach (var pipe in block.PipesWithBothEdges)
                {
                    TransformDoubleEdgedPipeIntoConnectingPipe(position, pipe);
                }

                ClearPipes(position, block.PipesWithBothEdges);

                return true;
            }

            return false;
        }

        private void ClearPipes(Coordinate position, IEnumerable<DoubleEdgedPipe> pipes)
        {
            var removingPipes = new List<DoubleEdgedPipe>(pipes);
            foreach (var pipe in removingPipes)
            {
                blueprint.RemovePipe(position, pipe);
            }
        }

        private void ClearPipes(Coordinate position, IEnumerable<ConnectingPipe> pipes)
        {
            var removingPipes = new List<ConnectingPipe>(pipes);
            foreach (var pipe in removingPipes)
            {
                blueprint.RemovePipe(position, pipe);
            }
        }

        public bool DeleteShipComponent(Coordinate position)
        {
            if (HasBlock(position) && GetBlock(position).HasShipComponent())
            {
                var block = GetBlock(position);

                var component = blueprint.RemoveShipComponent(position);
                component.RemoveAdditionalSetups(this);

                var pipes = block.PipesWithOneEdge.Select(pipe => pipe.Edge).ToList();
                ClearPipes(position, block.PipesWithOneEdge);
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
                            blueprint.PlaceShipComponent(position, emptyShipComponentFactory.Create());
                            TransformDoubleEdgedPipeIntoConnectingPipe(position, intersectingPipe);
                            TransformDoubleEdgedPipeIntoConnectingPipe(position, pipe);
                            ClearPipes(position, block.PipesWithBothEdges);
                        }
                        else
                        {
                            blueprint.PlacePipe(position, pipe);
                        }
                        return true;
                    }
                    else
                    {
                        TransformDoubleEdgedPipeIntoConnectingPipe(position, pipe);
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        public bool DeleteDoubleEdgedPipe(Coordinate position, DoubleEdgedPipe pipe)
        {
            var block = GetBlock(position);

            if (block != null)
            {
                if (CheckIfDoubleEdgeAlreadyExists(pipe, block))
                {
                    var p = GetDoubleEdgedPipe(pipe, block);
                    blueprint.RemovePipe(position, p);
                    return true;
                }

                if (CheckIfDoubleEdgedPipeCanBeComposedOfTwoConnectingPipes(pipe, block))
                {
                    var list = GetConnectingPipesThatComposeTwoEdgedPipe(pipe, block);
                    foreach (var p in list)
                    {
                        blueprint.RemovePipe(position, p);
                    }
                    return true;
                }
            }
            return false;
        }

        public bool AddConnectingPipe(Coordinate position, EdgeType edge)
        {
            var block = GetBlock(position);
            var pipe = new ConnectingPipe(edge);

            if (block != null)
            {
                if (!CheckIfConnectingPipeAlreadyExists(pipe, block) && block.HasShipComponent())
                {
                    blueprint.PlacePipe(position, pipe);
                    return true;
                }
            }
            return false;
        }

        public bool DeleteConnectingPipe(Coordinate position, ConnectingPipe pipe)
        {
            var block = GetBlock(position);

            if (block != null)
            {
                if (CheckIfConnectingPipeAlreadyExists(pipe, block))
                {
                    blueprint.RemovePipe(position, pipe);
                    return true;
                }
            }
            return false;
        }

        private DoubleEdgedPipe HasIntersectingPipes(IConstBlock block, DoubleEdgedPipe pipeToCheck)
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

        private void TransformDoubleEdgedPipeIntoConnectingPipe(Coordinate position, DoubleEdgedPipe pipe)
        {
            AddConnectingPipe(position, pipe.FirstEdge);
            AddConnectingPipe(position, pipe.SecondEdge);
        }

        private bool CheckIfDoubleEdgeAlreadyExists(DoubleEdgedPipe pipe, IConstBlock block)
        {
            return block.PipesWithBothEdges.Any(p => p.IsEqualTo(pipe));
        }

        private bool CheckIfConnectingPipeAlreadyExists(ConnectingPipe pipe, IConstBlock block)
        {
            return block.PipesWithOneEdge.Any(p => p.IsEqualTo(pipe));
        }

        private bool CheckIfDoubleEdgedPipeCanBeComposedOfTwoConnectingPipes(DoubleEdgedPipe pipe, IConstBlock block)
        {
            return block.PipesWithOneEdge.Select(p => p.Edge).ToList().Any(p => p == pipe.FirstEdge)
                && block.PipesWithOneEdge.Select(p => p.Edge).ToList().Any(p => p == pipe.SecondEdge);
        }

        private List<ConnectingPipe> GetConnectingPipesThatComposeTwoEdgedPipe(DoubleEdgedPipe pipe, IConstBlock block)
        {
            var list = new List<ConnectingPipe>();
            
            foreach (var connectingPipe in block.PipesWithOneEdge)
            {
                if (connectingPipe.Edge == pipe.FirstEdge || connectingPipe.Edge == pipe.SecondEdge)
                {
                    list.Add(connectingPipe);
                }
            }

            return list;
        }

        private DoubleEdgedPipe GetDoubleEdgedPipe(DoubleEdgedPipe pipe, IConstBlock block)
        {
            foreach (var p in block.PipesWithBothEdges)
            {
                if (p.IsEqualTo(pipe))
                {
                    return p;
                }
            }

            return null;
        }

        //todo: test
        public void AddRestrictor(IBlockRestrictor restrictor)
        {
            blockRestrictors.Add(restrictor);
        }

        //todo: test
        public void RemoveRestrictor(IBlockRestrictor restrictor)
        {
            blockRestrictors.Remove(restrictor);
        }
    }
}
