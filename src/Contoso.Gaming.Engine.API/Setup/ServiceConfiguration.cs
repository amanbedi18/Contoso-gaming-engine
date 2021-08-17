using Contoso.Gaming.Engine.API.Exceptions;
using Contoso.Gaming.Engine.API.Utilities;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Setup
{
    public static class ServiceConfiguration
    {
        /// <summary>
        /// Configures the problem details middleware.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="currentEnvironment">The current environment.</param>
        public static void ConfigureProblemDetailsMiddleware(this IServiceCollection services, IWebHostEnvironment currentEnvironment)
        {
            services.AddProblemDetails(setup =>
            {
                setup.IncludeExceptionDetails = (ctx, env) => currentEnvironment.IsDevelopment();
                setup.Map<NotFoundException>(exception => new NotFoundProblem
                {
                    Title = exception.Title,
                    Detail = exception.Detail,
                    //// map it from exception
                    Status = StatusCodes.Status404NotFound,
                    Type = ApiHelper.GetProblemDetailsTypeUrl(StatusCodes.Status404NotFound),
                    Instance = exception.Instance,
                    AdditionalInfo = exception.AdditionalInfo,
                });
            });
        }

        /// <summary>
        /// Configures the Validation Problem Details Middleware.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureValidationProblemDetailsMiddleware(this IServiceCollection services)
        {
            //// use this for validation probelm details instead of polluting the controller
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Title = APIMessageConstant.ServiceConfigurationBadRequestMessageTitle,
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Type = ApiHelper.GetProblemDetailsTypeUrl(StatusCodes.Status400BadRequest),
                        Detail = APIMessageConstant.ServiceConfigurationValidationFailedDetailMessage,
                    };
                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml },
                    };
                };
            });
        }
    }
}
