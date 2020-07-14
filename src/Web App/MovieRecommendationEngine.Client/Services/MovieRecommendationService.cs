using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MovieRecommendationEngine.Client.Abstractions;
using MovieRecommendationEngine.Client.Extensions;
using MovieRecommendationEngine.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Services
{
    /// <summary>
    /// MovieRecommendationService
    /// </summary>
    public class MovieRecommendationService
    {
        private readonly ITheMovieDBService _theMovieDBService;

        private readonly IMovieRecommendationEngineService _movieRecommendationEngineService;

        private readonly IMovieRecommendationEngineRepository _movieRecommendationEngineRepository;

        private readonly AppSettings _settings;

        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieRecommendationService"/> class.
        /// </summary>
        /// <param name="theMovieDBService">The movie database service.</param>
        /// <param name="movieRecommendationEngineService">The movie recommendation engine service.</param>
        /// <param name="movieRecommendationEngineRepository">The movie recommendation engine repository.</param>
        /// <param name="appSettings">The application settings.</param>
        /// <param name="logger">The logger.</param>
        /// <exception cref="ArgumentNullException">
        /// theMovieDBService
        /// or
        /// movieRecommendationEngineService
        /// or
        /// movieRecommendationEngineRepository
        /// or
        /// appSettings
        /// </exception>
        public MovieRecommendationService(ITheMovieDBService theMovieDBService,
            IMovieRecommendationEngineService movieRecommendationEngineService,
            IMovieRecommendationEngineRepository movieRecommendationEngineRepository,
            IOptions<AppSettings> appSettings,
            ILogger<MovieRecommendationService> logger)
        {
            _theMovieDBService = theMovieDBService ?? throw new ArgumentNullException(nameof(theMovieDBService));
            _movieRecommendationEngineService = movieRecommendationEngineService ?? throw new ArgumentNullException(nameof(movieRecommendationEngineService));
            _movieRecommendationEngineRepository = movieRecommendationEngineRepository ?? throw new ArgumentNullException(nameof(movieRecommendationEngineRepository));

            _logger = logger;
            _settings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        /// <summary>
        /// Gets the recommended movies.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> GetRecommendedMovies(int movieId)
        {
            var recommendendMovies = new List<Movie>();

            try
            {
                IEnumerable<int> similarMovies = await _movieRecommendationEngineService.GetRecommendationByContentFiltering(movieId, Constants.numberOfElements).ConfigureAwait(false);

                var tmdbMovie = await GetTmdbMovies(similarMovies).ConfigureAwait(false);

                if (tmdbMovie != null)
                {
                    recommendendMovies.AddRange(tmdbMovie);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, ex);
            }

            return recommendendMovies;
        }

        /// <summary>
        /// Gets the recommended movies.
        /// </summary>
        /// <param name="watchedMovies">The watched movies.</param>
        /// <param name="numberOfElements">The number of elements.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> GetRecommendedMovies(WatchedMoviesDto watchedMovies, int numberOfElements)
        {
            var recommendendMovies = new List<Movie>();

            try
            {
                var similarMovies = await _movieRecommendationEngineService.GetRecommendationByCollabFiltering(watchedMovies).ConfigureAwait(false);

                similarMovies = similarMovies?
                    .OrderBy(x => x)?
                    .Take(numberOfElements);

                var tmdbMovie = await GetTmdbMovies(similarMovies).ConfigureAwait(false);

                if (tmdbMovie != null)
                {
                    recommendendMovies.AddRange(tmdbMovie);
                }
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message, ex);
            }

            return recommendendMovies;
        }

        /// <summary>
        /// Gets the TMDB movies.
        /// </summary>
        /// <param name="similarMovies">The similar movies.</param>
        /// <returns></returns>
        private async Task<IEnumerable<Movie>> GetTmdbMovies(IEnumerable<int> similarMovies)
        {
            var recommendendMovies = new List<Movie>();

            if (similarMovies?.Any() == true)
            {
                foreach (var movie in similarMovies)
                {
                    var tmdbId = await _movieRecommendationEngineRepository.GetTmdbIdById(movie).ConfigureAwait(false);
                    var tmdbMovie = await _theMovieDBService.GetMovieById(tmdbId).ConfigureAwait(false);

                    if (tmdbMovie != null)
                    {
                        recommendendMovies.Add(tmdbMovie.ToMovie(_settings.ThemoviedbImageUrl));
                    }
                }
            }

            return recommendendMovies;
        }
    }
}
