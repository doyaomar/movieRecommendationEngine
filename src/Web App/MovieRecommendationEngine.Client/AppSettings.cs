namespace MovieRecommendationEngine.Client
{
    /// <summary>
    /// RecommendationEngineApiUrl
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        /// Gets or sets the recommendation engine API URL.
        /// </summary>
        /// <value>
        /// The recommendation engine API URL.
        /// </value>
        public string RecommendationEngineApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the themoviedb API URL.
        /// </summary>
        /// <value>
        /// The themoviedb API URL.
        /// </value>
        public string ThemoviedbApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the themoviedb image URL.
        /// </summary>
        /// <value>
        /// The themoviedb image URL.
        /// </value>
        public string ThemoviedbImageUrl { get; set; }

        /// <summary>
        /// Gets or sets the themoviedb API key.
        /// </summary>
        /// <value>
        /// The themoviedb API key.
        /// </value>
        public string ThemoviedbApiKey { get; set; }

        /// <summary>
        /// Gets or sets the themoviedb API key v4.
        /// </summary>
        /// <value>
        /// The themoviedb API key v4.
        /// </value>
        public string ThemoviedbApiKeyV4 { get; set; }
    }
}
