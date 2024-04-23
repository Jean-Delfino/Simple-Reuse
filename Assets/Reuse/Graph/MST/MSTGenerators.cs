using System.Collections.Generic;

namespace Reuse.Graph.MST
{
    public interface IMstGenerators
    {
        public (int node, int targetedByNode, float weigth)[] GenerateMst<T>(Node<T>[] nodes, int startingPoint = 0);
    }
}