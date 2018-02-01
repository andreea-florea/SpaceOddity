using Game.Enums;
using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game
{
    public class JetEngineFactory : IShipComponentFactory
    {
        private IConstBlock block;
        private EdgeType edgeType;

        public JetEngineFactory(EdgeType edgeType)
        {
            this.edgeType = edgeType;
        }

        public IShipComponent Create(IConstBlock block)
        {
            this.block = block;
            return new JetEngine(block, edgeType);
        }
    }
}
