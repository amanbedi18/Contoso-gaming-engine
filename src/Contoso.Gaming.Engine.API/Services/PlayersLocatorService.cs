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
            // source == destination
            var res = this.graphService.GetPathsAndWeights(Graph, source, destination);
            var routes = res.Select(s =>
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
            throw new NotImplementedException();
        }
    }
}
