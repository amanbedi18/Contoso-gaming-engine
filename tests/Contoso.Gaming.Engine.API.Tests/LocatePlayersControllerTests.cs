using Contoso.Gaming.Engine.API.Controllers.V1;
using Contoso.Gaming.Engine.API.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Contoso.Gaming.Engine.API.Tests
{
    public class LocatePlayersControllerTests
    {
        /// <summary>
        /// The Locate Players Controller.
        /// </summary>
        private readonly LocatePlayersController locatePlayersController;

        /// <summary>
        /// The mock players locator service.
        /// </summary>
        private readonly Mock<IPlayersLocatorService> mockPlayersLocatorService;

        /// <summary>
        /// The mock logger.
        /// </summary>
        private readonly Mock<ILogger<LocatePlayersController>> mockLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocatePlayersControllerTests"/> class.
        /// </summary>
        public LocatePlayersControllerTests()
        {
            //HeaderDictionary headerDictionary = new();
            //Mock<HttpResponse> response = new();
            //response.SetupGet(r => r.Headers).Returns(headerDictionary);

            //Mock<HttpContext> httpContext = new();
            //httpContext.SetupGet(a => a.Response).Returns(response.Object);

            //ControllerContext controllerContext = new()
            //{
            //    HttpContext = new DefaultHttpContext() { User = user },
            //};


            //this.locatePlayersController = new ContainersController(this._containerManager.Object)
            //{
            //    ControllerContext = controllerContext,
            //};
        }

        
        [Fact]
        public async Task GetAllPathsBetweenPlayersTest()
        {
        }

    }
}
