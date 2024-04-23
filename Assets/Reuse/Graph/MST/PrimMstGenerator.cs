using System;
using System.Collections.Generic;

namespace Reuse.Graph.MST
{
    public class PrimMstGenerator : IMstGenerators
    {
        private class PrimPriority
        {
            public float Weigth; //Or Priority
            public int Node;
            public int TargetedByNode;

            public PrimPriority(float weigth, int node, int targetedByNode)
            {
                Weigth = weigth;
                Node = node;
                TargetedByNode = targetedByNode;
            }
        }
        
        public (int node, int targetedByNode, float weigth)[] GenerateMst<T>(Node<T>[] nodes, int startingPoint = 0)
        {
            var edges = SetupEdges(nodes.Length, startingPoint);

            //It would be a lot better with a priority queue, but the current Unity could not handle it for some reason
            List<PrimPriority> connections = new();
            AddAllNodeConnections(nodes[startingPoint], startingPoint, edges, connections);
            
            while (connections.Count > 0)
            {
                var newConnection = GetSmallestConnection(connections, edges);

                if (newConnection == null) break;
                
                edges[newConnection.Node].targetedByNode = newConnection.TargetedByNode;
                edges[newConnection.Node].weigth = newConnection.Weigth;

                AddAllNodeConnections(nodes[newConnection.Node], newConnection.Node, edges, connections);
            }
            
            return edges;
        }

        private (int node, int targetedByNode, float weigth)[] SetupEdges(int nodeAmount, int startingPoint)
        {
            (int node, int targetedByNode, float weigth)[] edges = new (int, int, float)[nodeAmount];

            for (int i = 0; i < nodeAmount; i++)
            {
                edges[i].node = i;
                edges[i].targetedByNode = -1;
            }
            
            edges[startingPoint].targetedByNode = edges[startingPoint].node = startingPoint;
            return edges;
        }

        private void AddAllNodeConnections<T>(Node<T> node, int nodeIndex, (int node, int targetedByNode, float weigth)[] edges,
            List<PrimPriority> connections)
        {
            foreach (var edge in node.Edges)
            {
                var currentEdgeConnection = edges[edge.TargetNode];
                if(currentEdgeConnection.targetedByNode > -1) continue;
                
                connections.Add(new PrimPriority(edge.Weight, currentEdgeConnection.node, nodeIndex));
            }
        }

        private PrimPriority GetSmallestConnection(List<PrimPriority> connections,(int node, int targetedByNode, float weigth)[] edges)
        {
            float min = float.MaxValue;
            int index = -1;
            for (int i = 0; i < connections.Count; i++)
            {
                if (edges[connections[i].Node].targetedByNode > -1)
                {
                    connections.RemoveAt(i);
                    i--;
                    continue;
                }

                if (connections[i].Weigth < min)
                {
                    index = i;
                    min = connections[i].Weigth;
                }
            }

            if (index < 0) return null;
            
            var value = connections[index];
            connections.RemoveAt(index);
            
            return value;

        }
    }
}