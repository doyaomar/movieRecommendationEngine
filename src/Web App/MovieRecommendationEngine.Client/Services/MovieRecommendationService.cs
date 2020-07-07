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
    public class MovieRecommendationService
    {
        private readonly ITheMovieDBService _theMovieDBService;

        private readonly IMovieRecommendationEngineService _movieRecommendationEngineService;

        private readonly IMovieRecommendationEngineRepository _movieRecommendationEngineRepository;

        private readonly AppSettings _settings;

        public MovieRecommendationService(ITheMovieDBService theMovieDBService, IMovieRecommendationEngineService movieRecommendationEngineService, IMovieRecommendationEngineRepository movieRecommendationEngineRepository, IOptions<AppSettings> appSettings)
        {
            _theMovieDBService = theMovieDBService ?? throw new ArgumentNullException(nameof(theMovieDBService));
            _movieRecommendationEngineService = movieRecommendationEngineService ?? throw new ArgumentNullException(nameof(movieRecommendationEngineService));
            _movieRecommendationEngineRepository = movieRecommendationEngineRepository ?? throw new ArgumentNullException(nameof(movieRecommendationEngineRepository));

            _settings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public async Task<IEnumerable<Movie>> GetRecommendedMovies(int movieId)
        {
            var recommendendMovies = new List<Movie>();

            try
            {
                var similarMovies = await _movieRecommendationEngineService.GetRecommendationByContentFiltering(movieId, Constants.numberOfElements).ConfigureAwait(false);

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
            }
            catch (Exception ex)
            {
                throw;

                //add treatment later
            }

            return recommendendMovies;
        }
    }
}
