using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.DataStore
{
    public static class InMemoryGraph
    {
        private static bool isInitialized = false;

        public static Dictionary<string, List<Edge>> Graph;

        public static Dictionary<string, List<Edge>> GetInMemoryGraph(int vertices, int edges, string[] graphEdges, string[] graphVertices)
        {
            if(!graphEdges.Any() || !graphVertices.Any() || vertices == 0 || edges == 0)
            {
                // throw
                return null;
            }

            if(!isInitialized)
            {
                Graph = new Dictionary<string, List<Edge>>();

                for (int i = 0; i < vertices; i++)
                {
                    Graph.Add(graphVertices[i], new List<Edge>());
                }

                for (int i = 0; i < edges; i++)
                {
                    string[] parts = graphEdges[i].Split('@');
                    int wt = int.Parse(parts[2]);
                    Graph[parts[0]].Add(new Edge(parts[0], parts[1], wt));
                }

                isInitialized = true;
            }
            
            return Graph;
        }
    }
}
