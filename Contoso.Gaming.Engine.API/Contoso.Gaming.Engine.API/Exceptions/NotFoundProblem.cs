namespace Contoso.Gaming.Engine.API.Exceptions
{
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The Metadata Store not found problem.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ProblemDetails" />
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
