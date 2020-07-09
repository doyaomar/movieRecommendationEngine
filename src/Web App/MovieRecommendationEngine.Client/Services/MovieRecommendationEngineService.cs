using Microsoft.Extensions.Options;
using MovieRecommendationEngine.Client.Abstractions;
using MovieRecommendationEngine.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Services
{
    /// <summary>
    /// MovieRecommendationEngineService
    /// </summary>
    /// <seealso cref="MovieRecommendationEngine.Client.Abstractions.IMovieRecommendationEngineService" />
    public class MovieRecommendationEngineService : IMovieRecommendationEngineService
    {
        private readonly HttpClient _httpClient;

        private readonly AppSettings _appSettings;

        private readonly string _recommendationEngineApiBaseUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieRecommendationEngineService"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <exception cref="ArgumentNullException">
        /// appSettings
        /// or
        /// httpClient
        /// </exception>
        public MovieRecommendationEngineService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _recommendationEngineApiBaseUrl = $"{_appSettings.RecommendationEngineApiUrl }/movies/";
        }

        /// <summary>
        /// Gets the recommendation by collab filtering.
        /// </summary>
        /// <param name="watchedMovies">The watched movies.</param>
        /// <returns></returns>
        public async Task<IEnumerable<int>> GetRecommendationByCollabFiltering(WatchedMoviesDto watchedMovies)
        {
            if (!watchedMovies?.WatchedMovies?.Any() == true)
            {
                return null;
            }

            var uri = $"{_recommendationEngineApiBaseUrl}collab/recommendation";
            var stringContent = new StringContent(JsonConvert.SerializeObject(watchedMovies), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, stringContent).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<int>>(responseString);
        }

        /// <summary>
        /// Gets the recommendation by content filtering.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        /// <param name="numberOfElements">The number of elements.</param>
        /// <returns></returns>
        public async Task<IEnumerable<int>> GetRecommendationByContentFiltering(int movieId, int numberOfElements)
        {
            if (numberOfElements <= 0)
            {
                return null;
            }

            var uri = $"{_recommendationEngineApiBaseUrl}content/recommendation?movie_id={movieId}&number_of_elements={numberOfElements}";

            var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<int>>(responseString);
        }
    }
}
