using System.Collections.Generic;

namespace Reuse.Graph.MST
{
    public interface IMstGenerators
    {
        public (int node, int targetedByNode)[] GenerateMst(Node[] nodes, int startingPoint = 0);
    }
}