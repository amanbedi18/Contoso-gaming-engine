// -----------------------------------------------------------------------
// <copyright file="EnumSchemaFilter.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Utilities
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Microsoft.OpenApi.Any;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerGen;

    /// <summary>
    /// The Enum Schema Filter.
    /// </summary>
    /// <seealso cref="Swashbuckle.AspNetCore.SwaggerGen.ISchemaFilter" />
    [ExcludeFromCodeCoverage]
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Applies the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="context">The context.</param>
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                model.Enum.Clear();
                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(n => model.Enum.Add(new OpenApiString(n)));
            }
        }
    }
}
