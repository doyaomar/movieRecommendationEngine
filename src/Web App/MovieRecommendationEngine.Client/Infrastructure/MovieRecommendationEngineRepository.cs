using Microsoft.EntityFrameworkCore;
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
        private readonly MovieLensContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieRecommendationEngineRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="ArgumentNullException">context</exception>
        public MovieRecommendationEngineRepository(MovieLensContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Gets the movie by identifier.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        /// <returns></returns>
        public async Task<Movie> GetMovieById(int movieId)
        {
            return await _context.Movies
                //.Include(movie => movie.Links)
                .FirstOrDefaultAsync(movie => movie.MovieId == movieId)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the TMDB identifier by identifier.
        /// </summary>
        /// <param name="moviedId">The movied identifier.</param>
        /// <returns></returns>
        public async Task<int> GetTmdbIdById(int moviedId)
        {
            return await Task.FromResult(_context.Links
                .FirstOrDefault(link => link.MovieId == moviedId && link.TmdbId.HasValue)
                .TmdbId.Value
                ).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the movie by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> GetMovieByTitle(string title)
        {
            return await _context.Movies
                .Where(movie => movie.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync()
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the top.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        public async Task<IEnumerable<Movie>> GetTop(int top)
        {
            return await _context.Movies.OrderBy(x => x.MovieId).Take(top).ToListAsync().ConfigureAwait(false);
        }
    }
}
