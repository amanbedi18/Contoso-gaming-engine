using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Utilities
{
    [ExcludeFromCodeCoverage]
    public static class ApiHelper
    {
        /// <summary>
        /// Gets the problem details type URL.
        /// </summary>
        /// <param name="httpStatusCode">The HTTP status code.</param>
        /// <returns>The type.</returns>
        public static string GetProblemDetailsTypeUrl(int httpStatusCode)
        {
            string typeValue;
            switch (httpStatusCode)
            {
                case 409:
                    typeValue = "https://tools.ietf.org/html/rfc7231#section-6.5.8";
                    break;
                case 404:
                    typeValue = "https://tools.ietf.org/html/rfc7231#section-6.5.4";
                    break;
                case 500:
                    typeValue = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1";
                    break;
                case 400:
                    typeValue = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1";
                    break;
                default:
                    typeValue = "https://datatracker.ietf.org/doc/html";
                    break;
            }

            return typeValue;
        }
    }
}
