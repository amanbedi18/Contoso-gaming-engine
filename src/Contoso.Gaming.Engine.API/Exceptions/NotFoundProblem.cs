namespace Contoso.Gaming.Engine.API.Exceptions
{
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The Metadata Store not found problem.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ProblemDetails" />
    [ExcludeFromCodeCoverage]
    public class NotFoundProblem : ProblemDetails
    {
        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string AdditionalInfo { get; set; }
    }
}
