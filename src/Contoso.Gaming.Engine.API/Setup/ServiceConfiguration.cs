using Contoso.Gaming.Engine.API.Exceptions;
using Contoso.Gaming.Engine.API.Utilities;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;

namespace Contoso.Gaming.Engine.API.Setup
{
    [ExcludeFromCodeCoverage]
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
                    Status = StatusCodes.Status404NotFound,
                    Type = ApiHelper.GetProblemDetailsTypeUrl(StatusCodes.Status404NotFound),
                    Instance = exception.Instance,
                    AdditionalInfo = exception.AdditionalInfo,
                });
                setup.Map<ArgumentException>(exception => new ProblemDetails
                {
                    Title = exception.Message,
                    Detail = "Problem with arguments passed",
                    Status = StatusCodes.Status400BadRequest,
                    Type = ApiHelper.GetProblemDetailsTypeUrl(StatusCodes.Status400BadRequest),
                    Instance = exception.HelpLink,
                });
                setup.Map<Exception>(exception => new ProblemDetails
                {
                    Title = exception.Message,
                    Detail = "Exception occured",
                    Status = StatusCodes.Status400BadRequest,
                    Type = ApiHelper.GetProblemDetailsTypeUrl(StatusCodes.Status400BadRequest),
                    Instance = exception.HelpLink,
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
                        Title = "Validation failed for given inputs",
                        Instance = context.HttpContext.TraceIdentifier,
                        Status = StatusCodes.Status400BadRequest,
                        Type = ApiHelper.GetProblemDetailsTypeUrl(StatusCodes.Status400BadRequest),
                        Detail = context.HttpContext.Request.Path,
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
