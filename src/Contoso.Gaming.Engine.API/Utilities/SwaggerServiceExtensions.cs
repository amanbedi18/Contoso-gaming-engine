using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Contoso.Gaming.Engine.API.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class SwaggerServiceExtensions
    {
        /// <summary>
        /// Adds the swagger documentation.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>The service collection.</returns>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Contoso Gaming Engine API",
                    Version = "v1",
                    Description = "Find path between players via landmarks.",
                });
                options.TagActionsBy(api => new[] { api.GroupName });
                options.DocInclusionPredicate((name, api) => true);
                options.SchemaFilter<EnumSchemaFilter>();

                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                
                //// Set the comments path for the Swagger JSON and UI.
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            }).AddSwaggerGenNewtonsoftSupport();
            return services;
        }

        /// <summary>
        /// Uses the swagger documentation.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <returns>The app builder.</returns>
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Versioned API v1.0");
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
            return app;
        }
    }
}
