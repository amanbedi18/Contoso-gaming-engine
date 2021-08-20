// -----------------------------------------------------------------------
// <copyright file="RouteRequestDetails.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// The Route Request Details.
    /// </summary>
    /// <seealso cref="System.ComponentModel.DataAnnotations.IValidatableObject" />
    public class RouteRequestDetails : IValidatableObject
    {
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        [JsonProperty("source")]
        [Required]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        [JsonProperty("destination")]
        [Required]
        public string Destination { get; set; }

        /// <summary>
        /// Gets or sets the landmarks.
        /// </summary>
        /// <value>
        /// The landmarks.
        /// </value>
        [JsonProperty("landmarks")]
        public IEnumerable<string> Landmarks { get; set; }

        /// <summary>
        /// Determines whether the specified object is valid.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.Source) || string.IsNullOrEmpty(this.Destination))
            {
                validationResults.Add(new ValidationResult($"Source or destination cannot be empty", new[] { nameof(this.Source), nameof(this.Destination) }));
            }

            if (this.Source == this.Destination)
            {
                validationResults.Add(new ValidationResult($"Source cannot be same as destination", new[] { nameof(this.Source), nameof(this.Destination) }));
            }

            if (this.Landmarks != null && this.Landmarks.Any())
            {
                if (this.Source == this.Landmarks.First() && this.Destination == this.Landmarks.Last())
                {
                    validationResults.Add(new ValidationResult($"Source & destination are not required to be included in landmarks", new[] { nameof(this.Landmarks) }));
                }
            }

            return validationResults.AsEnumerable();
        }
    }
}
