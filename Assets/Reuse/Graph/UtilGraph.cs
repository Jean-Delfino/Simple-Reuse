using System.Collections.Generic;
using System.Linq;
using Reuse.Utils;
using UnityEngine;

namespace Reuse.Graph
{
    public static class UtilGraph
    {
        public static void RandomlyConnectByGraph<T>(Graph<T> graph, int maxConnectionsForNode, float minWeight, float maxWeight, bool isBy = true)
        {
            HashSet<int> missingConnectionNode = new();
            var nodes = graph.Nodes;
            
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].ClearEdges();
                missingConnectionNode.Add(i);
            }

            missingConnectionNode.Remove(0);
            
            var toConnectNodes = new Queue<int>();
            toConnectNodes.Enqueue(0);

            while (toConnectNodes.Count > 0)
            {
                var nodeIndex = toConnectNodes.Dequeue();
                var node = nodes[nodeIndex];
                var initialEdgeAmount = node.Edges.Count;
                
                for (var i = 0; i < maxConnectionsForNode - initialEdgeAmount; i++)
                {
                    var randomWeight = UtilRandom.GetRandomFloatInRange(minWeight, maxWeight);
                    int connection;
                    
                    if (missingConnectionNode.Count > 0)
                    {
                        var res = GetRandomInsideMissingConnections(nodes, missingConnectionNode, nodeIndex, maxConnectionsForNode);
                        
                        if(res.connection < 0) return;//Break don't change the code, but return is more correct 

                        connection = res.connection;
                        missingConnectionNode.Remove(connection);
                        
                        if(res.fromMissing) toConnectNodes.Enqueue(connection);
                    }
                    else
                    {
                        connection = GetRandomNodeDifferentFromEdges(nodes,nodeIndex, maxConnectionsForNode);
                        
                        if(connection < 0) return; //Break don't change the code, but return is more correct
                    }

                    node.AddEdge(connection, randomWeight);
                    if(isBy) nodes[connection].AddEdge(nodeIndex, randomWeight);
                }
            }
        }

        private static int GetRandomNodeDifferentFromEdges<T>(List<Node<T>> nodes, int actual, int maxConnections)
        {
            HashSet<int> validConnectionsNodes = new();

            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].Edges.Count >= maxConnections || i == actual) continue;
                
                validConnectionsNodes.Add(i);
            }

            foreach (var edge in nodes[actual].Edges)
            {
                validConnectionsNodes.Remove(edge.TargetNode);
            }

            return validConnectionsNodes.Count > 0 ? validConnectionsNodes.ElementAt(UtilRandom.RandomIndex(validConnectionsNodes.Count)) : -1;
        }

        private static (bool fromMissing, int connection) GetRandomInsideMissingConnections<T>(List<Node<T>> nodes, HashSet<int> missingConnectionNode, int actual, int maxConnections)
        {
            bool removed = missingConnectionNode.Remove(actual);

            if (missingConnectionNode.Count < 1)
            {
                if(removed) missingConnectionNode.Add(actual);
                return (false, GetRandomNodeDifferentFromEdges<T>(nodes, actual, maxConnections));
            }
            
            var index = UtilRandom.RandomIndex(missingConnectionNode.Count);
            var value = missingConnectionNode.ElementAt(index);

            if(removed) missingConnectionNode.Add(actual);

            return (true, value);

        }
    }
}