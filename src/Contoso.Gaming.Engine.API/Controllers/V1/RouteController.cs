// -----------------------------------------------------------------------
// <copyright file="RouteController.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Controllers.V1
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;
    using Contoso.Gaming.Engine.API.Entities;
    using Contoso.Gaming.Engine.API.Exceptions;
    using Contoso.Gaming.Engine.API.Services.Interfaces;
    using Contoso.Gaming.Engine.API.Utilities;
    using Microsoft.ApplicationInsights;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The Route Controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/locateplayers")]
    [ApiVersion("1.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "locateplayers")]
    public class RouteController : ControllerBase
    {
        /// <summary>
        /// The players locator service.
        /// </summary>
        private readonly IPlayersLocatorService playersLocatorService;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly TelemetryClient logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteController"/> class.
        /// </summary>
        /// <param name="playersLocatorService">The players locator service.</param>
        /// <param name="logger">The logger.</param>
        public RouteController(IPlayersLocatorService playersLocatorService, TelemetryClient logger)
        {
            this.playersLocatorService = playersLocatorService;
            this.logger = logger;
        }

        /// <summary>
        /// Get all possible routes with weights from source to destination.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1/locateplayers/A/E
        /// </remarks>
        /// <param name="source">The source id of the player.</param>
        /// <param name="destination">The destination id of the player.</param>
        /// <returns>Task <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        [Route("{source}/{destination}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllRoutes([FromRoute][Required] string source, [FromRoute][Required] string destination)
        {
            this.logger.TrackTrace($"GetAllRoutes requested for source: {source}, destination {destination} & trace id: {this.HttpContext.TraceIdentifier}");
            var routes = await this.playersLocatorService.FindAllRoutes(source, destination).ConfigureAwait(false);

            if (!routes.Any())
            {
                throw new NotFoundException(title: "Path not found", instance: this.HttpContext.TraceIdentifier, detail: $"No path found between {source} & {destination}", additionalInfo: APIMessageConstants.PathNotFoundMessage);
            }

            this.logger.TrackTrace($"Found {routes.Count()} routes for trace id: {this.HttpContext.TraceIdentifier}");
            return await Task.FromResult(this.Ok(routes));
        }

        /// <summary>
        /// Get all possible routes with weights from source to destination with required number of maximum hops.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     GET /api/v1/locateplayers/A/C/2
        /// </remarks>
        /// <param name="source">The source id of the player.</param>
        /// <param name="destination">The destination id of the player.</param>
        /// <param name="hops">The number of maximum hops.</param>
        /// <returns>Task <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        [Route("{source}/{destination}/{hops}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetRoutesWithHops([FromRoute][Required] string source, [FromRoute][Required] string destination, [FromRoute][Required] int hops)
        {
            this.logger.TrackTrace($"GetRoutesWithHops requested for source: {source}, destination {destination}, with required hops {hops} & trace id: {this.HttpContext.TraceIdentifier}");
            var routes = await this.playersLocatorService.FindAllRoutesWithHops(source, destination, hops).ConfigureAwait(false);

            if (!routes.Any())
            {
                throw new NotFoundException(title: "Path not found", instance: this.HttpContext.TraceIdentifier, detail: $"No path found between {source} & {destination}", additionalInfo: APIMessageConstants.PathNotFoundMessage);
            }

            this.logger.TrackTrace($"Found {routes.Count()} routes for trace id: {this.HttpContext.TraceIdentifier}");
            return await Task.FromResult(this.Ok(routes));
        }

        /// <summary>
        /// Post a message to find distance between source and destination via landmarks.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/v1/locateplayers/findroutes
        ///     {
        ///        "source": "A", #source should not be same as destination
        ///        "destination": "E"
        ///        "landmarks": { #should not start with source and end with destination, only contain landmarks
        ///             "B",
        ///             "C",
        ///             "D"
        ///         },
        ///     }
        /// </remarks>
        /// <param name="routeRequestDetails">The route request details.</param>
        /// <returns>Task <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [Route("findroutes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> FindRoutes([Required] RouteRequestDetails routeRequestDetails)
        {
            this.logger.TrackTrace($"Find routes requested for source: {routeRequestDetails.Source}, destination {routeRequestDetails.Destination}, against landmarks: {routeRequestDetails.Landmarks?.Count()} & trace id: {this.HttpContext.TraceIdentifier}");

            var routes = await this.playersLocatorService.FindRoutesAlongLandmarks(routeRequestDetails).ConfigureAwait(false);

            if (!routes.Any())
            {
                throw new NotFoundException(title: "Path not found", instance: this.HttpContext.TraceIdentifier, detail: $"No path found between {routeRequestDetails.Source} & {routeRequestDetails.Destination} via given landmarks.", additionalInfo: APIMessageConstants.PathNotFoundMessage);
            }

            this.logger.TrackTrace($"Found {routes.Count()} routes for trace id: {this.HttpContext.TraceIdentifier}");
            return await Task.FromResult(this.Ok(routes));
        }
    }
}
