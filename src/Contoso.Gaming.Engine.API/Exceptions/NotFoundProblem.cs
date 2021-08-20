// -----------------------------------------------------------------------
// <copyright file="NotFoundProblem.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Exceptions
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.AspNetCore.Mvc;

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
