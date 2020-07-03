using Microsoft.EntityFrameworkCore;
using MovieRecommendationEngine.Client.Abstractions;
using MovieRecommendationEngine.Client.Models;
using System;
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
            return await _context
                .Movies
                .Where(movie => movie.MovieId == movieId)
                .Include(movie => movie.Links)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        public async Task<int> GetTmdbIdById(int moviedId)
        {
            return await _context.
                Links
                .Where(link => link.MovieId == moviedId && link.TmdbId.HasValue)
                .Select(link => link.TmdbId.Value)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }
    }
}
