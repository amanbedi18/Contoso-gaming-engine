using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Entities
{
    public class RouteRequestDetails : IValidatableObject
    {
        [JsonProperty("source")]
        [Required]
        public string Source { get; set; }

        [JsonProperty("destination")]
        [Required]
        public string Destination { get; set; }
        
        [JsonProperty("landmarks")]
        public IEnumerable<string> Landmarks { get; set; }

        [JsonProperty("requiredHops")]
        [DefaultValue(0)]
        public int RequiredHops { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            if (Source == Destination)
            {
                validationResults.Add(new ValidationResult($"Source cannot be same as destination", new[] { nameof(this.Source), nameof(this.Destination) }));
            }

            if (RequiredHops < 0)
            {
                validationResults.Add(new ValidationResult($"Required Hops cannot be negative", new[] { nameof(this.RequiredHops) }));
            }

            return validationResults.AsEnumerable();
        }
    }
}
