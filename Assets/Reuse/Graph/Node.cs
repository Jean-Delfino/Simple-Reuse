using System;
using System.Collections.Generic;

namespace Reuse.Graph
{
    public class Node<T>
    {
        private T _data;

        public T Data => _data;
        
        private List<Edge> _edges = new();
        
        public List<Edge> Edges => _edges;
        
        public Node<T> DeepCopy()
        {
            Node<T> newNode = new Node<T>(this._data)
            {
                _edges = new List<Edge>(this.Edges.Count)
            };

            // Deep copy edges
            foreach (var edge in this.Edges)
            {
                newNode.Edges.Add(new Edge(edge.TargetNode, edge.Weight));
            }

            return newNode;
        }

        public Node(T data)
        {
            _data = data;
        }
        public void AddEdge(int targetNode, float weight)
        {
            _edges.Add(new Edge(targetNode, weight));
        }

        public Edge GetEdge(int edgeIndex)
        {
            if (edgeIndex < 0 || edgeIndex > _edges.Count - 1) throw new IndexOutOfRangeException();

            return _edges[edgeIndex];
        }
        public void ClearEdges()
        {
            _edges.Clear();
        }
    }
}