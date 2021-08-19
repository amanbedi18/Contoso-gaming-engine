using Contoso.Gaming.Engine.API.Entities;
using Contoso.Gaming.Engine.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Services.Interfaces
{
    public interface IPlayersLocatorService
    {
        Task<IEnumerable<WeightedRoutesModel>> FindAllRoutes(string source, string destination);

        Task<IEnumerable<WeightedRoutesModel>> FindRoutesAlongLandmarks(RouteRequestDetails routeRequestDetails);

        Task<IEnumerable<WeightedRoutesModel>> FindAllRoutesWithHops(string source, string destination, int hops);

    }
}
