using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Reuse.Graph
{
    public class Graph<T>
    {
        private readonly List<Node<T>> _nodes = new();

        public List<Node<T>> Nodes => _nodes;

        public void AddNode(T data)
        {
            _nodes.Add(new Node<T>(data));
        }

        public Node<T> GetNode(int nodeIndex)
        {
            if (nodeIndex < 0 || nodeIndex > _nodes.Count - 1) throw new IndexOutOfRangeException();

            return _nodes[nodeIndex];
        }

        public T GetNodeValue(int nodeIndex)
        {
            if (nodeIndex < 0 || nodeIndex > _nodes.Count - 1) throw new IndexOutOfRangeException();

            return _nodes[nodeIndex].Data;
        }

        public void RemakeAllEdges((int node, int targetedByNode, float weigth)[] newEdges, bool isBy)
        {
            foreach (var node in _nodes)
            {
                node.ClearEdges();
            }
        
            for (int i = 0; i < newEdges.Length; i++)
            {
                _nodes[newEdges[i].targetedByNode].AddEdge(newEdges[i].node, newEdges[i].weigth);
                
                if(isBy) _nodes[newEdges[i].node].AddEdge(newEdges[i].targetedByNode, newEdges[i].weigth);
            }
        }

        public List<(int node, int targetedByNode, float weigth)> RemakeAllEdgesReturnUnused((int node, int targetedByNode, float weigth)[] newEdges, bool isBy)
        {
            List<Node<T>> nodesCopy = _nodes.Select(node => node.DeepCopy()).ToList();
            
            RemakeAllEdges(newEdges, isBy);

            foreach (var node in nodesCopy)
            {
                node.Edges.RemoveAll(e => _nodes.Any(n => n.Edges.Contains(e)));
            }

            List<(int node, int targetedByNode, float weigth)> unusedEdges = new();

            for (var i = 0; i < nodesCopy.Count; i++)
            {
                foreach (var edge in nodesCopy[i].Edges)
                {
                    unusedEdges.Add((edge.TargetNode, i, edge.Weight));
                }
            }

            return unusedEdges;
        }

        public void PrintGraphConnections()
        {
            for (int i = 0; i < _nodes.Count; i++)
            {
                for (int j = 0; j < _nodes[i].Edges.Count; j++)
                { 
                    Debug.Log($"{i} para {_nodes[i].Edges[j].TargetNode} peso {_nodes[i].Edges[j].Weight}");
                }
            }
        }
    }
}