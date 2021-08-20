// -----------------------------------------------------------------------
// <copyright file="IGraphService.cs" company="Contoso Gaming">
// Copyright (c) Contoso Gaming. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace Contoso.Gaming.Engine.API.Services.Interfaces
{
    using System.Collections.Generic;
    using Contoso.Gaming.Engine.API.DataStore;

    /// <summary>
    /// The Graph Service Interface.
    /// </summary>
    public interface IGraphService
    {
        /// <summary>
        /// Gets all paths with weights.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <returns>Returns list of paths.</returns>
        List<string> GetAllPathsWithWeights(Dictionary<string, List<Edge>> graph, string src, string dest);

        /// <summary>
        /// Gets all paths with weightsvia landmarks.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="landmarks">The landmarks.</param>
        /// <returns>Returns list of paths.</returns>
        List<string> GetAllPathsWithWeightsviaLandmarks(Dictionary<string, List<Edge>> graph, string src, string dest, List<string> landmarks);

        /// <summary>
        /// Gets all paths with weightsvia landmarksand hops.
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <param name="maxHops">The maximum hops.</param>
        /// <returns>Returns list of paths.</returns>
        List<string> GetAllPathsWithWeightsviaLandmarksandHops(Dictionary<string, List<Edge>> graph, string src, string dest, int maxHops);

        /// <summary>
        /// Determines whether [has path between vertices] [the specified graph].
        /// </summary>
        /// <param name="graph">The graph.</param>
        /// <param name="src">The source.</param>
        /// <param name="dest">The dest.</param>
        /// <returns>
        ///   <c>true</c> if [has path between vertices] [the specified graph]; otherwise, <c>false</c>.
        /// </returns>
        bool HasPathBetweenVertices(Dictionary<string, List<Edge>> graph, string src, string dest);
    }
}
