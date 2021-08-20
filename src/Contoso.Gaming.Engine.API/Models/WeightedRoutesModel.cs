// -----------------------------------------------------------------------
// <copyright file="WeightedRoutesModel.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// The Weighted Routes Model.
    /// </summary>
    public class WeightedRoutesModel
    {
        /// <summary>
        /// Gets or sets the route value.
        /// </summary>
        /// <value>
        /// The route value.
        /// </value>
        [JsonProperty("routeValue")]
        public string RouteValue { get; set; }

        /// <summary>
        /// Gets or sets the route weight.
        /// </summary>
        /// <value>
        /// The route weight.
        /// </value>
        [JsonProperty("routeWeight")]
        public int RouteWeight { get; set; }
    }
}
