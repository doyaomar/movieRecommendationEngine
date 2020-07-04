using Microsoft.EntityFrameworkCore;
using MovieRecommendationEngine.Client.Abstractions;
using MovieRecommendationEngine.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Infrastructure
{
    public class MovieRecommendationEngineRepository : IMovieRecommendationEngineRepository
    {
        private readonly MovieLensContext _context;

        public MovieRecommendationEngineRepository(MovieLensContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Movie> GetMovieById(int movieId)
        {
            return await _context.Movies
                //.Include(movie => movie.Links)
                .FirstOrDefaultAsync(movie => movie.MovieId == movieId)
                .ConfigureAwait(false);
        }

        public async Task<int> GetTmdbIdById(int moviedId)
        {
            return await Task.FromResult(_context.Links
                .FirstOrDefault(link => link.MovieId == moviedId && link.TmdbId.HasValue)
                .TmdbId.Value
                ).ConfigureAwait(false);
        }

        public async Task<IEnumerable<Movie>> GetMovieByTitle(string title)
        {
            return await _context.Movies
                .Where(movie => movie.Title.ToLower().Contains(title.ToLower()))
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
