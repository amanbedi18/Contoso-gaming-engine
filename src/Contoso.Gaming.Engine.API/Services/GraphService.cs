using Contoso.Gaming.Engine.API.DataStore;
using Contoso.Gaming.Engine.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Services
{
    public class GraphService : IGraphService
    {
        public List<string> GetPathsAndWeights(Dictionary<string, List<Edge>> graph, string src, string dest)
        {
            var paths = new List<string>();
            getAllPathsAndWeights(graph, src, dest, new HashSet<string>(), paths, src, 0);
            return paths;
        }


        //1.	The distance between landmarks via the route A-B-C.
        //2.	The distance between landmarks via the route A-E-B-C-D.
        //3.	The distance between landmarks via the route A-E-D.
        private void getPathsAndWeightsviaLandmarks(Dictionary<string, List<Edge>> graph, string src, string dest, HashSet<string> isVisited, List<string> allPaths, string currentPath, int weightSoFar, string[] pt, int idx)
        {
            if (src == dest && idx == pt.Length)
            {
                Console.WriteLine(currentPath);
                allPaths.Add(currentPath + "@" + weightSoFar);
                weightSoFar = 0;
            }

            isVisited.Add(src);
            foreach (var edge in graph[src])
            {
                if (isVisited.Contains(edge.Nbr) == false)
                {
                    if (edge.Nbr != dest && idx < pt.Length && edge.Nbr != pt[idx]) continue;
                    else
                    {
                        getPathsAndWeightsviaLandmarks(graph, edge.Nbr, dest, isVisited, allPaths, currentPath + edge.Nbr.ToString(), weightSoFar + edge.Wt, pt, idx + 1);
                    }
                }
            }

            isVisited.Remove(src);
        }

        // find all paths between A & C
        // limitation of recursive call
        private void getAllPathsAndWeights(Dictionary<string, List<Edge>> graph, string src, string dest, HashSet<string> isVisited, List<string> allPaths, string currentPath, int weightSoFar)
        {
            if (src == dest)
            {
                Console.WriteLine(currentPath);
                allPaths.Add(currentPath + "@" + weightSoFar);
                weightSoFar = 0;
            }

            isVisited.Add(src);
            foreach (var edge in graph[src])
            {
                if (!isVisited.Contains(edge.Nbr))
                {
                    getAllPathsAndWeights(graph, edge.Nbr, dest, isVisited, allPaths, currentPath + edge.Nbr.ToString(), weightSoFar + edge.Wt);

                }
            }

            isVisited.Remove(src);
        }

        // path between A & C with maximum of 2 hops
        private void getPathsAndWeightsWithGivenHops(Dictionary<string, List<Edge>> graph, string src, string dest, HashSet<string> isVisited, List<string> allPaths, string currentPath, int weightSoFar, int ct, int maxHops)
        {
            if (src == dest && ct == maxHops)
            {
                Console.WriteLine(currentPath);
                allPaths.Add(currentPath + "@" + weightSoFar);
                weightSoFar = 0;
            }

            isVisited.Add(src);
            foreach (var edge in graph[src])
            {
                if (!isVisited.Contains(edge.Nbr))
                {
                    getPathsAndWeightsWithGivenHops(graph, edge.Nbr, dest, isVisited, allPaths, currentPath + edge.Nbr.ToString(), weightSoFar + edge.Wt, ct + 1, maxHops);

                }
            }

            isVisited.Remove(src);
        }

        private bool hasPathBetweenVerices(Dictionary<string, List<Edge>> graph, string src, string dest, HashSet<string> isVisited)
        {
            if (src == dest)
            {
                return true;
            }

            isVisited.Add(src);
            foreach (var edge in graph[src])
            {
                if (isVisited.Contains(edge.Nbr) == false)
                {
                    bool res = hasPathBetweenVerices(graph, edge.Nbr, dest, isVisited);
                    if (res == true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
