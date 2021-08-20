// -----------------------------------------------------------------------
// <copyright file="Edge.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.DataStore
{
    /// <summary>
    /// The Edge.
    /// </summary>
    public class Edge
    {
        /// <summary>
        /// The source.
        /// </summary>
        private readonly string src;

        /// <summary>
        /// The neighbour.
        /// </summary>
        private readonly string nbr;

        /// <summary>
        /// The weight.
        /// </summary>
        private readonly int wt;

        /// <summary>
        /// Initializes a new instance of the <see cref="Edge"/> class.
        /// </summary>
        /// <param name="src">The source.</param>
        /// <param name="nbr">The NBR.</param>
        /// <param name="wt">The wt.</param>
        public Edge(string src, string nbr, int wt)
        {
            this.src = src;
            this.nbr = nbr;
            this.wt = wt;
        }

        /// <summary>
        /// Gets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Src => this.src;

        /// <summary>
        /// Gets the neighbour.
        /// </summary>
        /// <value>
        /// The neighbour.
        /// </value>
        public string Nbr => this.nbr;

        /// <summary>
        /// Gets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public int Wt => this.wt;
    }
}
