using MovieRecommendationEngine.Client.Models;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Abstractions
{
    /// <summary>
    /// ITheMovieDBService
    /// </summary>
    public interface ITheMovieDBService
    {
        /// <summary>
        /// Gets the movie by identifier.
        /// </summary>
        /// <param name="tmdbId">The TMDB identifier.</param>
        /// <returns></returns>
        Task<MovieTmdbDto> GetMovieById(int tmdbId);
    }
}
