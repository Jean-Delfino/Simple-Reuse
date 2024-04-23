
namespace Reuse.Graph
{
    public class Edge
    {
        // private T _data;
        //
        // public T Data => _data;
        
        private int _targetNode;
        private float _weight;
        public int TargetNode => _targetNode;
        public float Weight => _weight;

        public Edge(int targetNode, float weight)
        {
            _targetNode = targetNode;
            _weight = weight;
        }
    }
}