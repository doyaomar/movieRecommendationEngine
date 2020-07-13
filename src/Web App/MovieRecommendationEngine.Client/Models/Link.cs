namespace MovieRecommendationEngine.Client.Models
{
    /// <summary>
    /// Link
    /// </summary>
    public partial class Link
    {
        /// <summary>
        /// Gets or sets the movie identifier.
        /// </summary>
        /// <value>
        /// The movie identifier.
        /// </value>
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets the imdb identifier.
        /// </summary>
        /// <value>
        /// The imdb identifier.
        /// </value>
        public int ImdbId { get; set; }

        /// <summary>
        /// Gets or sets the TMDB identifier.
        /// </summary>
        /// <value>
        /// The TMDB identifier.
        /// </value>
        public int? TmdbId { get; set; }

        /// <summary>
        /// Gets or sets the movie.
        /// </summary>
        /// <value>
        /// The movie.
        /// </value>
        public virtual Movie Movie { get; set; }
    }
}
