using Contoso.Gaming.Engine.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Gaming.Engine.API.Tests
{
    public class PlayersLocatorServiceTests
    {
        /// <summary>
        /// The players locator service
        /// </summary>
        private readonly PlayersLocatorService playersLocatorService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersLocatorServiceTests"/> class.
        /// </summary>
        public PlayersLocatorServiceTests()
        {
            this.playersLocatorService = new PlayersLocatorService(new GraphService());
        }

        [Fact]
        public async Task GetAllRoutesBetweenPlayersTest()
        {
            var testresponse = await this.playersLocatorService.FindAllRoutes("A", "C").ConfigureAwait(false);
            Assert.NotNull(testresponse);
        }

        [Fact]
        public async Task GetAllRoutesBetweenPlayersInvalidInputTest()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await this.playersLocatorService.FindAllRoutes("A", "A").ConfigureAwait(false));
            await Assert.ThrowsAsync<ArgumentException>(async () => await this.playersLocatorService.FindAllRoutes("A", "Z").ConfigureAwait(false));
            await Assert.ThrowsAsync<ArgumentException>(async () => await this.playersLocatorService.FindAllRoutes("X", "Z").ConfigureAwait(false));
            await Assert.ThrowsAsync<ArgumentException>(async () => await this.playersLocatorService.FindAllRoutes("", "").ConfigureAwait(false));
        }
     
        [Fact]
        public async Task GetAllRoutesBetweenPlayersviaLandmarksTest()
        {
            var testresponse = await this.playersLocatorService.FindRoutesAlongLandmarks(new Entities.RouteRequestDetails() { Source = "A", Destination = "C", RequiredHops = 2 }).ConfigureAwait(false);

            Assert.NotNull(testresponse);
            //Assert.Equal("ABC", finalReturnedObject[0].RouteValue);
            //Assert.Equal(34, finalReturnedObject[0].RouteWeight);
        }

        [Fact]
        public async Task GetAllRoutesBetweenPlayersviaGivenHopsTest()
        {
            var testresponse = await this.playersLocatorService.FindRoutesAlongLandmarks(new Entities.RouteRequestDetails() { Source = "A", Destination = "C", Landmarks = new List<string> () { "D" } }).ConfigureAwait(false);

            //var okobjectResult = Assert.IsType<OkObjectResult>(testresponse);

            Assert.NotNull(testresponse);
            //Assert.Equal("ABC", finalReturnedObject[0].RouteValue);
            //Assert.Equal(34, finalReturnedObject[0].RouteWeight);
        }

        [Fact]
        public async Task GetAllRoutesBetweenPlayersviaLandmarksInvalidInputTest()
        {
            await Assert.ThrowsAsync<ArgumentException>(async () => await this.playersLocatorService.FindRoutesAlongLandmarks(new Entities.RouteRequestDetails() { Source = "", Destination = "" }).ConfigureAwait(false));
            await Assert.ThrowsAsync<ArgumentException>(async () => await this.playersLocatorService.FindRoutesAlongLandmarks(new Entities.RouteRequestDetails() { Source = "A", Destination = "A" }).ConfigureAwait(false));
            await Assert.ThrowsAsync<ArgumentException>(async () => await this.playersLocatorService.FindRoutesAlongLandmarks(new Entities.RouteRequestDetails() { Source = "X", Destination = "Y" }).ConfigureAwait(false));
            await Assert.ThrowsAsync<ArgumentException>(async () => await this.playersLocatorService.FindRoutesAlongLandmarks(new Entities.RouteRequestDetails() { Source = "A", Destination = "B", Landmarks = new List<string>() { "X" } }).ConfigureAwait(false));
            await Assert.ThrowsAsync<ArgumentException>(async () => await this.playersLocatorService.FindRoutesAlongLandmarks(new Entities.RouteRequestDetails() { Source = "A", Destination = "B", Landmarks = new List<string>() { "X" }, RequiredHops = 9 }).ConfigureAwait(false));
        }
    }
}
