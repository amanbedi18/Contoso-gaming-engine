// -----------------------------------------------------------------------
// <copyright file="HttpResponseExceptionFilter.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Exceptions
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.ApplicationInsights;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// The Http Response Exception Filter.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IActionFilter" />
    [ExcludeFromCodeCoverage]
    public class HttpResponseExceptionFilter : IActionFilter
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly TelemetryClient logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseExceptionFilter"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public HttpResponseExceptionFilter(TelemetryClient logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Called before the action executes, after model binding is complete.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext" />.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext" />.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                this.logger.TrackException(context.Exception, new Dictionary<string, string>() { { "TraceId", context.HttpContext.TraceIdentifier } });
            }
        }
    }
}
