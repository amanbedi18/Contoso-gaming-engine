// -----------------------------------------------------------------------
// <copyright file="InMemoryGraph.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.DataStore
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The In Memory Graph.
    /// </summary>
    public static class InMemoryGraph
    {
        /// <summary>
        /// The is initialized.
        /// </summary>
        private static bool isInitialized = false;

        /// <summary>
        /// Gets the graph.
        /// </summary>
        /// <value>
        /// The graph.
        /// </value>
        public static Dictionary<string, List<Edge>> Graph { get; private set; }

        /// <summary>
        /// Gets the in memory graph.
        /// </summary>
        /// <param name="vertices">The vertices.</param>
        /// <param name="edges">The edges.</param>
        /// <param name="graphEdges">The graph edges.</param>
        /// <param name="graphVertices">The graph vertices.</param>
        /// <returns>Returns in memory graph.</returns>
        /// <exception cref="System.ArgumentException">Graph inputs are not correct.</exception>
        public static Dictionary<string, List<Edge>> GetInMemoryGraph(int vertices, int edges, string[] graphEdges, string[] graphVertices)
        {
            if (!graphEdges.Any() || !graphVertices.Any() || vertices == 0 || edges == 0)
            {
                throw new ArgumentException("Graph inputs are not correct");
            }

            if (!isInitialized)
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
