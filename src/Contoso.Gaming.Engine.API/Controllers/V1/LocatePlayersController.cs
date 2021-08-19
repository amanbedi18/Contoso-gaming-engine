using Contoso.Gaming.Engine.API.Entities;
using Contoso.Gaming.Engine.API.Exceptions;
using Contoso.Gaming.Engine.API.Services.Interfaces;
using Contoso.Gaming.Engine.API.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Controllers.V1
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/locateplayers")]
    [ApiVersion("1.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "locateplayers")]
    public class LocatePlayersOnMapController : ControllerBase
    {
        private readonly IPlayersLocatorService playersLocatorService;
        private readonly ILogger<LocatePlayersOnMapController> logger;

        public LocatePlayersOnMapController(IPlayersLocatorService playersLocatorService, ILogger<LocatePlayersOnMapController> logger)
        {
            this.playersLocatorService = playersLocatorService;
            this.logger = logger;
        }

        /// <summary>
        /// Get possible routes from source to destination.
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// // change locateplayers to routecontroller, controller name should not be in the route
        ///     GET /api/v1/locateplayers/{source}/{destination}
        /// </remarks>
        /// <param name="source">The source id of the player.</param>
        /// <param name="destination">The destination id of the player.</param>
        /// <returns>Task <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet]
        [Route("{source}/{destination}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRoutes([FromRoute][Required] string source, [FromRoute][Required] string destination)
        {
            var routes = await this.playersLocatorService.FindAllRoutes(source, destination).ConfigureAwait(false);

            if (!routes.Any())
            {
                throw new NotFoundException(title: "Path not found", instance: this.HttpContext.TraceIdentifier, detail: $"No path found between {source} & {destination}", additionalInfo: APIMessageConstant.PathNotFoundMessage);
            }

            return await Task.FromResult(this.Ok(routes));
        }

        /// <summary>
        /// Post a message to find distance between landmarks via a given route and number of hops (optional).
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/v1/locateplayers/findroutes
        ///     {
        ///        "source": "A", #source should not be same as destination
        ///        "destination": "E"
        ///        "landmarks": {  #optional parameter
        ///             "B",
        ///             "C",
        ///             "D"
        ///         },
        ///         "requiredHops": 3 #optional parameter, defaults to zero if not provided
        ///     }
        /// </remarks>
        /// <param name="routeRequestDetails">The route request details.</param>
        /// <returns>Task <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPost]
        [Route("findroutes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> FindRoutes(RouteRequestDetails routeRequestDetails)
        {
            var routes = await this.playersLocatorService.FindRoutesAlongLandmarks(routeRequestDetails).ConfigureAwait(false);

            if (!routes.Any())
            {
                throw new NotFoundException(title: "Path not found", instance: this.HttpContext.TraceIdentifier, detail: $"No path found between {routeRequestDetails.Source} & {routeRequestDetails.Destination} via given landmarks and hops.", additionalInfo: APIMessageConstant.PathNotFoundMessage);
            }

            return await Task.FromResult(this.Ok(routes));
        }
    }
}
