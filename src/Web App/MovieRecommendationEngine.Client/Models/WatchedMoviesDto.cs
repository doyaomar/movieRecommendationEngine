using Newtonsoft.Json;
using System.Collections.Generic;

namespace MovieRecommendationEngine.Client.Models
{
    /// <summary>
    /// WatchedMoviesDto
    /// </summary>
    public class WatchedMoviesDto
    {
        /// <summary>
        /// Gets or sets the watched movies.
        /// </summary>
        /// <value>
        /// The watched movies.
        /// </value>
        [JsonProperty("watched_movies")]
        public IEnumerable<RatingDto> WatchedMovies { get; set; }
    }
}
