using Contoso.Gaming.Engine.API.Entities;
using Contoso.Gaming.Engine.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Controllers.V1
{
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "locateplayers")]
    public class LocatePlayersController : ControllerBase
    {
        private readonly IPlayersLocatorService playersLocatorService;
        private readonly ILogger<LocatePlayersController> logger;

        public LocatePlayersController(IPlayersLocatorService playersLocatorService, ILogger<LocatePlayersController> logger)
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
            // source destination check

            var routes = await this.playersLocatorService.FindAllRoutes(source, destination).ConfigureAwait(false);

            //if (appMetaDataStore == null)
            //{
            //    throw new MetaDataStoreNotFoundException(title: "Metadata not found", instance: "id", detail: "Metadata is not present", additionalInfo: APIMessageConstant.notfound);
            //}

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
            return await Task.FromResult(this.Ok(new { message = "Executed successfully." }));
        }
    }
}
