using System.Collections.Generic;

namespace Reuse.Graph
{
    public class Node
    {
        private List<Edge> _edges;
        
        public List<Edge> Edges => _edges;

    }
}