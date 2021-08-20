// -----------------------------------------------------------------------
// <copyright file="IPlayersLocatorService.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contoso.Gaming.Engine.API.Entities;
    using Contoso.Gaming.Engine.API.Models;

    /// <summary>
    /// The Players Locator Service interface.
    /// </summary>
    public interface IPlayersLocatorService
    {
        /// <summary>
        /// Finds all routes.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>Returns weighted routes.</returns>
        Task<IEnumerable<WeightedRoutesModel>> FindAllRoutes(string source, string destination);

        /// <summary>
        /// Finds the routes along landmarks.
        /// </summary>
        /// <param name="routeRequestDetails">The route request details.</param>
        /// <returns>Returns weighted routes.</returns>
        Task<IEnumerable<WeightedRoutesModel>> FindRoutesAlongLandmarks(RouteRequestDetails routeRequestDetails);

        /// <summary>
        /// Finds all routes with hops.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="hops">The hops.</param>
        /// <returns>Returns weighted routes.</returns>
        Task<IEnumerable<WeightedRoutesModel>> FindAllRoutesWithHops(string source, string destination, int hops);
    }
}
