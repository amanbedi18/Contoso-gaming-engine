using Contoso.Gaming.Engine.API.Exceptions;
using Contoso.Gaming.Engine.API.Services;
using Contoso.Gaming.Engine.API.Services.Interfaces;
using Contoso.Gaming.Engine.API.Setup;
using Contoso.Gaming.Engine.API.Utilities;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Contoso.Gaming.Engine.API
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Gets the current environment.
        /// </summary>
        /// <value>
        /// The current environment.
        /// </value>
        public IWebHostEnvironment CurrentEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureProblemDetailsMiddleware(this.CurrentEnvironment);
            var appInsightsConnectionString = Configuration["ApplicationInsights:InstrumentationKey"];
            services.AddApplicationInsightsTelemetry(appInsightsConnectionString);

            services.AddSingleton<IGraphService, GraphService>();
            // transient == scope?
            services.AddScoped<IPlayersLocatorService, PlayersLocatorService>();

            services.AddControllers().AddNewtonsoftJson();

            services.AddControllers();
            services.AddControllers(options => options.Filters.Add(typeof(HttpResponseExceptionFilter)));

            services.AddSwaggerDocumentation();

            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                //// If the client hasn't specified the API version in the request, use the default API version number
                config.AssumeDefaultVersionWhenUnspecified = true;
                //// Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });

            services.ConfigureValidationProblemDetailsMiddleware();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseProblemDetails();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contoso.Gaming.Engine.API v1"));
            }

            app.UseHttpsRedirection();

            app.UsePathBase("/cge");
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/cge/swagger/v1/swagger.json", "Contoso.Gaming.Engine.Api v1"));
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
