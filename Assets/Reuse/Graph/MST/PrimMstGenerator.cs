using System;
using System.Collections.Generic;

namespace Reuse.Graph.MST
{
    public class PrimMstGenerator : IMstGenerators
    {
        private class PrimPriority
        {
            public int Priority;
            public int Node;
            public int TargetedByNode;

            public PrimPriority(int priority, int node, int targetedByNode)
            {
                Priority = priority;
                Node = node;
                TargetedByNode = targetedByNode;
            }
        }
        
        public (int node, int targetedByNode)[] GenerateMst(Node[] nodes, int startingPoint = 0)
        {
            var edges = SetupEdges(nodes.Length, startingPoint);

            //It would be a lot better with a priority queue, but the current Unity could not handle it for some reason
            List<PrimPriority> connections = new();
            AddAllNodeConnections(nodes[startingPoint], startingPoint, edges, connections);
            
            while (connections.Count > 0)
            {
                var newConnection = GetSmallestConnection(connections);

                edges[newConnection.Node].targetedByNode = newConnection.TargetedByNode;

                AddAllNodeConnections(nodes[newConnection.Node], newConnection.Node, edges, connections);
            }
            
            return null;
        }

        private (int node, int targetedByNode)[] SetupEdges(int nodeAmount, int startingPoint)
        {
            (int node, int targetedByNode)[] edges = new (int, int)[nodeAmount];

            for (int i = 0; i < nodeAmount; i++)
            {
                edges[i].node = i;
                edges[i].targetedByNode = -1;
            }
            
            edges[startingPoint].targetedByNode = edges[startingPoint].node = startingPoint;
            return edges;
        }

        private void AddAllNodeConnections(Node node, int nodeIndex, (int node, int targetedByNode)[] edges,
            List<PrimPriority> connections)
        {
            foreach (var edge in node.Edges)
            {
                var currentEdgeConnection = edges[edge.TargetNode];
                if(currentEdgeConnection.targetedByNode != -1) continue;
                
                connections.Add(new PrimPriority(edge.Weight, currentEdgeConnection.node, nodeIndex));
            }
        }

        private PrimPriority GetSmallestConnection(List<PrimPriority> connections)
        {
            float min = float.MaxValue;
            int index = -1;
            for (int i = 0; i < connections.Count; i++)
            {
                if (connections[i].TargetedByNode > -1)
                {
                    connections.RemoveAt(i);
                    i--;
                    continue;
                }

                if (connections[i].Priority < min)
                {
                    index = i;
                }
            }

            return index > 0 ? connections[index] : null;
        }
    }
}