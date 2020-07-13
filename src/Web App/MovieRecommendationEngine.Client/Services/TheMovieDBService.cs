using Microsoft.Extensions.Options;
using MovieRecommendationEngine.Client.Abstractions;
using MovieRecommendationEngine.Client.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Services
{
    public class TheMovieDBService : ITheMovieDBService
    {
        private readonly HttpClient _httpClient;

        private readonly AppSettings _appSettings;

        private readonly string _theMovieDbApiBaseUrl;

        public TheMovieDBService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            _theMovieDbApiBaseUrl = $"{_appSettings.ThemoviedbApiUrl}/3/movie/";
        }

        public async Task<MovieTmdbDto> GetMovieById(int tmdbId)
        {
            var uri = $"{_theMovieDbApiBaseUrl}{tmdbId}?api_key={_appSettings.ThemoviedbApiKey}";

            var response = await _httpClient.GetAsync(uri).ConfigureAwait(false);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<MovieTmdbDto>(responseAsString);
        }
    }
}
