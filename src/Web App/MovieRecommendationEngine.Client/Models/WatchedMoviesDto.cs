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
        /// Gets or sets the ratings.
        /// </summary>
        /// <value>
        /// The ratings.
        /// </value>
        [JsonProperty("watched_movies")]
        public List<RatingDto> Ratings { get; set; }
    }
}
