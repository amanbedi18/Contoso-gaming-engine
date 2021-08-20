// -----------------------------------------------------------------------
// <copyright file="NotFoundException.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Exceptions
{
    using System;

    /// <summary>
    /// The not found exception.
    /// </summary>
    /// <seealso cref="System.Exception" />
    /// <seealso cref="Contoso.Gaming.Engine.API.Exceptions.ICustomException" />
    public class NotFoundException : Exception, ICustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotFoundException"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="instance">The instance.</param>
        /// <param name="detail">The detail.</param>
        /// <param name="additionalInfo">The additional information.</param>
        public NotFoundException(string title, string instance, string detail, string additionalInfo)
        {
            this.Detail = detail;
            this.Title = title;
            this.AdditionalInfo = additionalInfo;
            this.Instance = instance;
        }

        /// <summary>
        /// Gets or sets the additional information.
        /// </summary>
        /// <value>
        /// The additional information.
        /// </value>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Gets or sets the detail.
        /// </summary>
        /// <value>
        /// The detail.
        /// </value>
        public string Detail { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public string Instance { get; set; }
    }
}
