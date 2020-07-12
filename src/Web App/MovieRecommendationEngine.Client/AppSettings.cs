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

        /// <summary>
        /// Gets or sets the connection strings.
        /// </summary>
        /// <value>
        /// The connection strings.
        /// </value>
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    /// <summary>
    /// ConnectionStrings
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// Gets or sets the movie lens database.
        /// </summary>
        /// <value>
        /// The movie lens database.
        /// </value>
        public string MovieLensDatabase { get; set; }
    }
}
