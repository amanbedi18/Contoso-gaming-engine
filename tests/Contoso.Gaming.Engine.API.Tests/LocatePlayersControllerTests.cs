using Contoso.Gaming.Engine.API.Controllers.V1;
using Contoso.Gaming.Engine.API.Entities;
using Contoso.Gaming.Engine.API.Exceptions;
using Contoso.Gaming.Engine.API.Models;
using Contoso.Gaming.Engine.API.Services.Interfaces;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Gaming.Engine.API.Tests
{
    public class LocatePlayersControllerTests
    {
        /// <summary>
        /// The Locate Players Controller.
        /// </summary>
        private readonly RouteController locatePlayersController;

        /// <summary>
        /// The mock players locator service.
        /// </summary>
        private readonly Mock<IPlayersLocatorService> mockPlayersLocatorService;

        /// <summary>
        /// The mock logger.
        /// </summary>
        private readonly TelemetryClient mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocatePlayersControllerTests"/> class.
        /// </summary>
        public LocatePlayersControllerTests()
        {
            ControllerContext controllerContext = new()
            {
                HttpContext = new DefaultHttpContext(),
            };

            this.mockLogger = new TelemetryClient();
            this.mockPlayersLocatorService = new Mock<IPlayersLocatorService>();

            this.locatePlayersController = new RouteController(this.mockPlayersLocatorService.Object, this.mockLogger)
            {
                ControllerContext = controllerContext,
            };
        }
        
        [Fact]
        public async Task GetAllPathsBetweenPlayersTest()
        {
            this.mockPlayersLocatorService.Setup(x => x.FindAllRoutes(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new List<WeightedRoutesModel>() { new WeightedRoutesModel() { RouteValue = "ABC", RouteWeight = 34 } });

            var testresponse = await this.locatePlayersController.GetAllRoutes("A", "C").ConfigureAwait(false);

            var okobjectResult = Assert.IsType<OkObjectResult>(testresponse);
            var finalReturnedObject = okobjectResult.Value as List<WeightedRoutesModel>;

            Assert.NotNull(finalReturnedObject);
            Assert.Single(finalReturnedObject);
            Assert.Equal("ABC", finalReturnedObject[0].RouteValue);
            Assert.Equal(34, finalReturnedObject[0].RouteWeight);
        }

        [Fact]
        public async Task GetAllPathsBetweenPlayersThrowsExceptionTest()
        {
            this.mockPlayersLocatorService.Setup(x => x.FindAllRoutes(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new List<WeightedRoutesModel>());
            await Assert.ThrowsAsync<NotFoundException>(async () => await this.locatePlayersController.GetAllRoutes("X", "Z").ConfigureAwait(false));
        }

        [Fact]
        public async Task FindRoutesBetweenPlayersTest()
        {
            this.mockPlayersLocatorService.Setup(x => x.FindRoutesAlongLandmarks(It.IsAny<RouteRequestDetails>())).ReturnsAsync(new List<WeightedRoutesModel>() { new WeightedRoutesModel() { RouteValue = "AED", RouteWeight = 36 } });

            var testresponse = await this.locatePlayersController.FindRoutes(new RouteRequestDetails() { Source = "A", Destination = "D" }).ConfigureAwait(false);

            var okobjectResult = Assert.IsType<OkObjectResult>(testresponse);
            var finalReturnedObject = okobjectResult.Value as List<WeightedRoutesModel>;

            Assert.NotNull(finalReturnedObject);
            Assert.Single(finalReturnedObject);
            Assert.Equal("AED", finalReturnedObject[0].RouteValue);
            Assert.Equal(36, finalReturnedObject[0].RouteWeight);
        }

        [Fact]
        public async Task FindRoutesBetweenPlayersViaLandmarksTest()
        {
            this.mockPlayersLocatorService.Setup(x => x.FindRoutesAlongLandmarks(It.IsAny<RouteRequestDetails>())).ReturnsAsync(new List<WeightedRoutesModel>() { new WeightedRoutesModel() { RouteValue = "ABE", RouteWeight = 39 } });

            var testresponse = await this.locatePlayersController.FindRoutes(new RouteRequestDetails() { Source = "A", Destination = "E", Landmarks = new List<string>() { "B" } }).ConfigureAwait(false);

            var okobjectResult = Assert.IsType<OkObjectResult>(testresponse);
            var finalReturnedObject = okobjectResult.Value as List<WeightedRoutesModel>;

            Assert.NotNull(finalReturnedObject);
            Assert.Single(finalReturnedObject);
            Assert.Equal("ABE", finalReturnedObject[0].RouteValue);
            Assert.Equal(39, finalReturnedObject[0].RouteWeight);
        }

        [Fact]
        public async Task FindRoutesBetweenPlayersThrowsExceptionTest()
        {
            this.mockPlayersLocatorService.Setup(x => x.FindRoutesAlongLandmarks(new RouteRequestDetails() { Source = "A", Destination = "B"})).ReturnsAsync(new List<WeightedRoutesModel>());
            await Assert.ThrowsAsync<NotFoundException>(async () => await this.locatePlayersController.FindRoutes(new RouteRequestDetails() { Source = "A", Destination = "D" }).ConfigureAwait(false));
        }
    }
}
