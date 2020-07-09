using Newtonsoft.Json;

namespace MovieRecommendationEngine.Client.Models
{
    /// <summary>
    /// RatingDto
    /// </summary>
    public class RatingDto
    {
        /// <summary>
        /// Gets or sets the movie identifier.
        /// </summary>
        /// <value>
        /// The movie identifier.
        /// </value>
        [JsonProperty("movieId")]
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        [JsonProperty("rating")]
        public short Rating { get; set; }
    }
}
