using Contoso.Gaming.Engine.API.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Services.Interfaces
{
    public interface IGraphService
    {
        List<string> GetAllPathsWithWeights(Dictionary<string, List<Edge>> graph, string src, string dest);

        List<string> GetAllPathsWithWeightsviaLandmarks(Dictionary<string, List<Edge>> graph, string src, string dest, List<string> landmarks);

        List<string> GetAllPathsWithWeightsviaLandmarksandHops(Dictionary<string, List<Edge>> graph, string src, string dest, int maxHops);

         bool HasPathBetweenVertices(Dictionary<string, List<Edge>> graph, string src, string dest);
    }
}
