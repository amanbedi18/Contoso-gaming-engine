// -----------------------------------------------------------------------
// <copyright file="GraphService.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Services
{
    using System;
    using System.Collections.Generic;
    using Contoso.Gaming.Engine.API.DataStore;
    using Contoso.Gaming.Engine.API.Services.Interfaces;

    /// <summary>
    /// The Graph Service.
    /// </summary>
    /// <seealso cref="Contoso.Gaming.Engine.API.Services.Interfaces.IGraphService" />
    public class GraphService : IGraphService
    {
        /// <summary>
        /// Gets all paths with weights.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <returns>Returns list of paths.</returns>
        public List<string> GetAllPathsWithWeights(Dictionary<string, List<Edge>> graph, string src, string dest)
        {
            var paths = new List<string>();
            this.getAllPathsWithWeights(graph, src, dest, new HashSet<string>(), paths, src, 0);
            return paths;
        }

        /// <summary>
        /// Gets all paths with weightsvia landmarks.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="landmarks">The landmarks.</param>
        /// <returns>Returns list of paths.</returns>
        public List<string> GetAllPathsWithWeightsviaLandmarks(Dictionary<string, List<Edge>> graph, string src, string dest, List<string> landmarks)
        {
            var paths = new List<string>();
            this.getPathsAndWeightsviaLandmarks(graph, src, dest, new HashSet<string>(), paths, src, 0, landmarks, 0);
            return paths;
        }

        /// <summary>
        /// Gets all paths with weightsvia landmarksand hops.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="maxHops">The maximum hops.</param>
        /// <returns>Returns list of paths.</returns>
        public List<string> GetAllPathsWithWeightsviaLandmarksandHops(Dictionary<string, List<Edge>> graph, string src, string dest, int maxHops)
        {
            var paths = new List<string>();
            this.getPathsAndWeightsWithGivenHops(graph, src, dest, new HashSet<string>(), paths, src, 0, 0, maxHops);
            return paths;
        }

        /// <summary>
        /// Determines whether [has path between vertices] [the specified graph].
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <returns>
        ///   <c>true</c> if [has path between vertices] [the specified graph]; otherwise, <c>false</c>.
        /// </returns>
        public bool HasPathBetweenVertices(Dictionary<string, List<Edge>> graph, string src, string dest)
        {
            return this.hasPathBetweenVerices(graph, src, dest, new HashSet<string>());
        }

        //// 1.	The distance between landmarks via the route A-B-C.
        //// 2.	The distance between landmarks via the route A-E-B-C-D.
        //// 3.	The distance between landmarks via the route A-E-D.
        /// <summary>
        /// Gets the paths and weightsvia landmarks.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="isVisited">The is visited.</param>
        /// <param name="allPaths">All paths.</param>
        /// <param name="currentPath">The current path.</param>
        /// <param name="weightSoFar">The weight so far.</param>
        /// <param name="landmarks">The landmarks.</param>
        /// <param name="idx">The index.</param>
        private void getPathsAndWeightsviaLandmarks(Dictionary<string, List<Edge>> graph, string src, string dest, HashSet<string> isVisited, List<string> allPaths, string currentPath, int weightSoFar, List<string> landmarks, int idx)
        {
            if (src == dest && idx == landmarks.Count + 1)
            {
                Console.WriteLine(currentPath);
                allPaths.Add(currentPath + "@" + weightSoFar);
                return;
            }

            isVisited.Add(src);
            foreach (var edge in graph[src])
            {
                if (isVisited.Contains(edge.Nbr) == false)
                {
                    if (edge.Nbr != dest && idx < landmarks.Count && edge.Nbr != landmarks[idx])
                    {
                        continue;
                    }
                    else
                    {
                        this.getPathsAndWeightsviaLandmarks(graph, edge.Nbr, dest, isVisited, allPaths, currentPath + edge.Nbr.ToString(), weightSoFar + edge.Wt, landmarks, idx + 1);
                    }
                }
            }

            isVisited.Remove(src);
        }

        //// find all paths between A & C
        //// TODO: limitation of recursive call
        /// <summary>
        /// Gets all paths with weights.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="isVisited">The is visited.</param>
        /// <param name="allPaths">All paths.</param>
        /// <param name="currentPath">The current path.</param>
        /// <param name="weightSoFar">The weight so far.</param>
        private void getAllPathsWithWeights(Dictionary<string, List<Edge>> graph, string src, string dest, HashSet<string> isVisited, List<string> allPaths, string currentPath, int weightSoFar)
        {
            if (src == dest)
            {
                Console.WriteLine(currentPath);
                allPaths.Add(currentPath + "@" + weightSoFar);
                return;
            }

            isVisited.Add(src);
            foreach (var edge in graph[src])
            {
                if (!isVisited.Contains(edge.Nbr))
                {
                    this.getAllPathsWithWeights(graph, edge.Nbr, dest, isVisited, allPaths, currentPath + edge.Nbr.ToString(), weightSoFar + edge.Wt);
                }
            }

            isVisited.Remove(src);
        }

        // path between A & C with maximum of 2 hops
        /// <summary>
        /// Gets the paths and weights with given hops.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="isVisited">The is visited.</param>
        /// <param name="allPaths">All paths.</param>
        /// <param name="currentPath">The current path.</param>
        /// <param name="weightSoFar">The weight so far.</param>
        /// <param name="ct">The ct.</param>
        /// <param name="maxHops">The maximum hops.</param>
        private void getPathsAndWeightsWithGivenHops(Dictionary<string, List<Edge>> graph, string src, string dest, HashSet<string> isVisited, List<string> allPaths, string currentPath, int weightSoFar, int ct, int maxHops)
        {
            if (ct > maxHops + 1)
            {
                return;
            }

            if (ct == maxHops + 1 && src == dest)
            {
                Console.WriteLine(currentPath);
                allPaths.Add(currentPath + "@" + weightSoFar);
                return;
            }

            isVisited.Add(src);
            foreach (var edge in graph[src])
            {
                if (!isVisited.Contains(edge.Nbr))
                {
                    this.getPathsAndWeightsWithGivenHops(graph, edge.Nbr, dest, isVisited, allPaths, currentPath + edge.Nbr.ToString(), weightSoFar + edge.Wt, ct + 1, maxHops);
                }
            }

            isVisited.Remove(src);
        }

        /// <summary>
        /// Determines whether [has path between verices] [the specified graph].
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="isVisited">The is visited.</param>
        /// <returns>
        ///   <c>true</c> if [has path between verices] [the specified graph]; otherwise, <c>false</c>.
        /// </returns>
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
                    bool res = this.hasPathBetweenVerices(graph, edge.Nbr, dest, isVisited);
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
