using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieRecommendationEngine.Client.Abstractions;
using MovieRecommendationEngine.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Infrastructure
{
    /// <summary>
    /// MovieRecommendationEngineRepository
    /// </summary>
    /// <seealso cref="MovieRecommendationEngine.Client.Abstractions.IMovieRecommendationEngineRepository" />
    public class MovieRecommendationEngineRepository : IMovieRecommendationEngineRepository
    {
        private readonly AppSettings _appSettings;

        private DbContextOptionsBuilder<MovieLensContext> _optionsBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieRecommendationEngineRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public MovieRecommendationEngineRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));

            _optionsBuilder = new DbContextOptionsBuilder<MovieLensContext>();
            _optionsBuilder.UseSqlServer(_appSettings.ConnectionStrings.MovieLensDatabase);
        }

        /// <summary>
        /// Gets the movie by identifier.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        /// <returns></returns>
        public async Task<Movie> GetMovieById(int movieId)
        {
            using (var _context = new MovieLensContext(_optionsBuilder.Options))
            {
                return await _context.Movies
                        //.Include(movie => movie.Links)
                        .FirstOrDefaultAsync(movie => movie.MovieId == movieId);
            }

        }

        /// <summary>
        /// Gets the TMDB identifier by identifier.
        /// </summary>
        /// <param name="moviedId">The movied identifier.</param>
        /// <returns></returns>
        public async Task<int> GetTmdbIdById(int moviedId)
        {
            using (var _context = new MovieLensContext(_optionsBuilder.Options))
            {
                return await Task.FromResult(_context.Links
                .FirstOrDefault(link => link.MovieId == moviedId && link.TmdbId.HasValue)
                .TmdbId.Value
                );
            }
        }

        /// <summary>
        /// Finds the movie by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> FindMovieByTitle(string title)
        {
            using (var _context = new MovieLensContext(_optionsBuilder.Options))
            {
                return await _context.Movies
                .Where(movie => movie.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync();
            }
        }

        /// <summary>
        /// Gets the top.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> GetTop(int top)
        {
            using (var _context = new MovieLensContext(_optionsBuilder.Options))
            {
                return await _context.Movies.OrderBy(x => x.MovieId)
                .Take(top)
                .ToListAsync();
            }
        }
    }
}
