using MovieRecommendationEngine.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Abstractions
{
    /// <summary>
    /// IMovieRecommendationEngineRepository
    /// </summary>
    public interface IMovieRecommendationEngineRepository
    {
        /// <summary>
        /// Gets the movie by identifier.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        /// <returns></returns>
        Task<Movie> GetMovieById(int movieId);

        /// <summary>
        /// Gets the TMDB identifier by identifier.
        /// </summary>
        /// <param name="moviedId">The movied identifier.</param>
        /// <returns></returns>
        Task<int> GetTmdbIdById(int moviedId);

        /// <summary>
        /// Finds the movie by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns></returns>
        Task<IEnumerable<Movie>> FindMovieByTitle(string title);

        /// <summary>
        /// Gets the top.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <returns></returns>
        Task<IEnumerable<Movie>> GetTop(int top);
    }
}
