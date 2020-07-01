using Microsoft.Extensions.Options;
using MovieRecommendationEngine.Client.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Services
{
    public class MovieRecommendationEngineService : IMovieRecommendationEngineService
    {
        private readonly HttpClient _httpClient;

        private readonly AppSettings _appSettings;

        public MovieRecommendationEngineService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value ?? throw new ArgumentNullException(nameof(appSettings));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
    }
}
