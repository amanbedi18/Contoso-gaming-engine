using Contoso.Gaming.Engine.API.DataStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Services.Interfaces
{
    public interface IGraphService
    {
        List<string> GetPathsAndWeights(Dictionary<string, List<Edge>> graph, string src, string dest);
    }
}
