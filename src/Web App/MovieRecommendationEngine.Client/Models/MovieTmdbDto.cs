using Newtonsoft.Json;

namespace MovieRecommendationEngine.Client.Models
{
    /// <summary>
    /// MovieTmdbDto
    /// </summary>
    public class MovieTmdbDto
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the poster path.
        /// </summary>
        /// <value>
        /// The poster path.
        /// </value>
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        /// <summary>
        /// Gets or sets the backdrop path.
        /// </summary>
        /// <value>
        /// The backdrop path.
        /// </value>
        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        /// <summary>
        /// Gets or sets the homepage.
        /// </summary>
        /// <value>
        /// The homepage.
        /// </value>
        public string Homepage { get; set; }

        /// <summary>
        /// Gets or sets the overview.
        /// </summary>
        /// <value>
        /// The overview.
        /// </value>
        public string Overview { get; set; }
    }
}
