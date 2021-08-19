using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class HttpResponseExceptionFilter : IActionFilter
    {
        private readonly TelemetryClient logger;

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
            if(context.Exception!=null)
            this.logger.TrackException(context.Exception, new Dictionary<string, string>() { {"TraceId", context.HttpContext.TraceIdentifier } });
        }
    }
}
