// -----------------------------------------------------------------------
// <copyright file="PlayersLocatorService.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contoso.Gaming.Engine.API.DataStore;
    using Contoso.Gaming.Engine.API.Entities;
    using Contoso.Gaming.Engine.API.Models;
    using Contoso.Gaming.Engine.API.Services.Interfaces;

    /// <summary>
    /// The Players Locator Service.
    /// </summary>
    /// <seealso cref="Contoso.Gaming.Engine.API.Services.Interfaces.IPlayersLocatorService" />
    public class PlayersLocatorService : IPlayersLocatorService
    {
        /// <summary>
        /// The graph.
        /// </summary>
        private static Dictionary<string, List<Edge>> graph;

        /// <summary>
        /// The graph edges.
        /// </summary>
        private static string[] graphEdges = new[] { "A@B@3", "B@C@9", "C@D@3", "D@E@6", "A@D@4", "D@A@5", "C@E@2", "A@E@4", "E@B@1", };
        //// "B@F@2", "E@C@3", "A@C@3", "A@K@1", "K@C@2"

        /// <summary>
        /// The graph vertices.
        /// </summary>
        private static string[] graphVertices = new[] { "A", "B", "C", "D", "E", };
        //// "F", "K"

        /// <summary>
        /// The vertices.
        /// </summary>
        private static int vertices = 5;

        /// <summary>
        /// The edges.
        /// </summary>
        private static int edges = 9;

        /// <summary>
        /// The graph service.
        /// </summary>
        private readonly IGraphService graphService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersLocatorService"/> class.
        /// </summary>
        /// <param name="graphService">The graph service.</param>
        public PlayersLocatorService(IGraphService graphService)
        {
            this.graphService = graphService;
            graph = InMemoryGraph.GetInMemoryGraph(vertices, edges, graphEdges, graphVertices);
        }

        /// <summary>
        /// Finds all routes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>
        /// Returns weighted routes.
        /// </returns>
        public Task<IEnumerable<WeightedRoutesModel>> FindAllRoutes(string source, string destination)
        {
            this.ValidateInputs(source, destination);

            var result = this.graphService.GetAllPathsWithWeights(graph, source, destination);
            var routes = this.MapModel(result);
            return Task.FromResult(routes);
        }

        /// <summary>
        /// Finds the routes along landmarks.
        /// </summary>
        /// <param name="routeRequestDetails">The route request details.</param>
        /// <returns>
        /// Returns weighted routes.
        /// </returns>
        public Task<IEnumerable<WeightedRoutesModel>> FindRoutesAlongLandmarks(RouteRequestDetails routeRequestDetails)
        {
            this.ValidateInputs(routeRequestDetails.Source, routeRequestDetails.Destination);
            this.ValidateRouteRequestDetails(routeRequestDetails);
            var result = new List<string>();
            result = this.graphService.GetAllPathsWithWeightsviaLandmarks(graph, routeRequestDetails.Source, routeRequestDetails.Destination, routeRequestDetails.Landmarks.ToList());
            var routes = this.MapModel(result);
            return Task.FromResult(routes);
        }

        /// <summary>
        /// Finds all routes with hops.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="hops">The hops.</param>
        /// <returns>
        /// Returns weighted routes.
        /// </returns>
        public Task<IEnumerable<WeightedRoutesModel>> FindAllRoutesWithHops(string source, string destination, int hops)
        {
            this.ValidateInputs(source, destination);
            this.ValidateHops(hops);

            var result = new List<string>();

            for (int i = 1; i <= hops; i++)
            {
                var tempresult = this.graphService.GetAllPathsWithWeightsviaLandmarksandHops(graph, source, destination, i);
                result.AddRange(tempresult);
            }

            var routes = this.MapModel(result);
            return Task.FromResult(routes);
        }

        /// <summary>
        /// Validates the hops.
        /// </summary>
        /// <param name="hops">The hops.</param>
        /// <exception cref="System.ArgumentException">Hops cannot be negative.</exception>
        private void ValidateHops(int hops)
        {
            if (hops < 0)
            {
                throw new ArgumentException($"Hops cannot be negative.");
            }
        }

        /// <summary>
        /// Maps the model.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>Returns list of weighted routes model.</returns>
        private IEnumerable<WeightedRoutesModel> MapModel(List<string> result)
        {
            return result.Select(s =>
            {
                var parts = s.Split('@');
                return new WeightedRoutesModel()
                {
                    RouteValue = parts[0],
                    RouteWeight = int.Parse(parts[1]),
                };
            });
        }

        /// <summary>
        /// Validates the route request details.
        /// </summary>
        /// <param name="routeRequestDetails">The route request details.</param>
        /// <exception cref="System.ArgumentException">
        /// Landmarks cannot be null or empty
        /// or
        /// Landmark: {landmark} not found in the graph.
        /// </exception>
        private void ValidateRouteRequestDetails(RouteRequestDetails routeRequestDetails)
        {
            if (routeRequestDetails.Landmarks == null || !routeRequestDetails.Landmarks.Any())
            {
                throw new ArgumentException($"Landmarks cannot be null or empty");
            }

            foreach (var landmark in routeRequestDetails.Landmarks)
            {
                if (!graphVertices.Contains(landmark))
                {
                    throw new ArgumentException($"Landmark: {landmark} not found in the graph.");
                }
            }
        }

#pragma warning disable CS1570 // XML comment has badly formed XML
/// <summary>
        /// Validates the inputs.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <exception cref="System.ArgumentException">
        /// Source & destination cannot be the same. Wrong parameter{source}
        /// or
        /// Source or destination cannot be empty
        /// or
        /// Source: {source} not found in the graph.
        /// or
        /// Destination: {destination} not found in the graph.
        /// </exception>
        private void ValidateInputs(string source, string destination)
#pragma warning restore CS1570 // XML comment has badly formed XML
        {
            if (source == destination)
            {
                throw new ArgumentException($"Source & destination cannot be the same. Wrong parameter{source}");
            }

            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(destination))
            {
                throw new ArgumentException("Source or destination cannot be empty");
            }

            if (!graphVertices.Contains(source))
            {
                throw new ArgumentException($"Source: {source} not found in the graph.");
            }

            if (!graphVertices.Contains(destination))
            {
                throw new ArgumentException($"Destination: {destination} not found in the graph.");
            }
        }
    }
}
