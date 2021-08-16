using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Utilities
{
    public static class APIMessageConstant
    {
        /// <summary>
        /// The name exists.
        /// </summary>
        public static readonly string NameExists = "name already exists.";

        /// <summary>
        /// The name not found.
        /// </summary>
        public static readonly string NameNotFound = "name with the id not found.";

        /// <summary>
        /// Processing error.
        /// </summary>
        public static readonly string NameProcessingError = "processing error.";

        /// <summary>
        /// The service configuration bad request message.
        /// </summary>
        public static readonly string ServiceConfigurationBadRequestMessageTitle = "Bad Request";

        /// <summary>
        /// The service configuration validation failed message.
        /// </summary>
        public static readonly string ServiceConfigurationValidationFailedDetailMessage = "Validation failed, check errors.";
    }
}
