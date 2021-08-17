using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Gaming.Engine.API.Models
{
    public class WeightedRoutesModel
    {
        [JsonProperty("routeValue")]
        public string RouteValue { get; set; }

        [JsonProperty("routeWeight")]
        public int RouteWeight { get; set; }
    }
}
