using Contoso.Gaming.Engine.API.DataStore;
using Contoso.Gaming.Engine.API.Entities;
using Contoso.Gaming.Engine.API.Models;
using Contoso.Gaming.Engine.API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Services
{
    public class PlayersLocatorService : IPlayersLocatorService
    {
        private static Dictionary<string, List<Edge>> Graph;
        private static string[] graphEdges = new[] { "A@B@3", "B@C@9", "C@D@3", "D@E@6", "A@D@4", "D@A@5", "C@E@2", "A@E@4", "E@B@1" };
        private static string[] graphVertices = new[] { "A", "B", "C", "D", "E" };
        private static int vertices = 5;
        private static int edges = 9;

        private readonly IGraphService graphService;

        public PlayersLocatorService(IGraphService graphService)
        {
            this.graphService = graphService;
            Graph = InMemoryGraph.GetInMemoryGraph(vertices, edges, graphEdges, graphVertices);
        }

        public async Task<IEnumerable<WeightedRoutesModel>> FindAllRoutes(string source, string destination)
        {
            ValidateInputs(source, destination);

            var result = this.graphService.GetAllPathsWithWeights(Graph, source, destination);
            var routes = result.Select(s =>
            {
                var parts = s.Split('@');
                return new WeightedRoutesModel() 
                {
                    RouteValue = parts[0],
                    RouteWeight = int.Parse(parts[1]),
                };
            });

            return routes;
        }

        public async Task<IEnumerable<WeightedRoutesModel>> FindRoutesAlongLandmarks(RouteRequestDetails routeRequestDetails)
        {
            ValidateInputs(routeRequestDetails.Source, routeRequestDetails.Destination);
            ValidateRouteRequestDetails(routeRequestDetails);
            var result = new List<string>();

            if(routeRequestDetails.RequiredHops > 0)
            {
                result = this.graphService.GetAllPathsWithWeightsviaLandmarksandHops(Graph, routeRequestDetails.Source, routeRequestDetails.Destination, routeRequestDetails.RequiredHops);
            }
            else
            {
                result = this.graphService.GetAllPathsWithWeightsviaLandmarks(Graph, routeRequestDetails.Source, routeRequestDetails.Destination, routeRequestDetails.Landmarks.ToList());
            }

            var routes = result.Select(s =>
            {
                var parts = s.Split('@');
                var path = parts[0].ToCharArray();
                if (path[0].ToString() == routeRequestDetails.Source && path[path.Length - 1].ToString() == routeRequestDetails.Destination)
                {
                    return new WeightedRoutesModel()
                    {
                        RouteValue = parts[0],
                        RouteWeight = int.Parse(parts[1]),
                    };
                }

                return null;
            });

            return routes;
        }

        private void ValidateRouteRequestDetails(RouteRequestDetails routeRequestDetails)
        {
            if(routeRequestDetails.RequiredHops == 0)
            {
                if (routeRequestDetails.Landmarks == null || !routeRequestDetails.Landmarks.Any())
                {
                    throw new ArgumentException($"Landmarks cannot be null or empty");
                }
                else
                {
                    foreach (var landmark in routeRequestDetails.Landmarks)
                    {
                        if (!graphVertices.Contains(landmark))
                        {
                            throw new ArgumentException($"Landmark: {landmark} not found in the graph.");
                        }
                    }
                }
            }

            if(routeRequestDetails.RequiredHops > vertices - 2)
            {
                throw new ArgumentException($"Hops: {routeRequestDetails.RequiredHops} cannot be a more than max available.");
            }
        }

        private void ValidateInputs(string source, string destination)
        {
            if (source == destination)
            {
                throw new ArgumentException($"Source & destination cannot be the same. Wrong parameter{source}");
            }

            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(destination))
            {
                throw new ArgumentException("Source or destination cannot be empty");
            }

            if(!graphVertices.Contains(source))
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
