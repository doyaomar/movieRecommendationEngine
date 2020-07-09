using MovieRecommendationEngine.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Abstractions
{
    /// <summary>
    /// IMovieRecommendationEngineService
    /// </summary>
    public interface IMovieRecommendationEngineService
    {
        /// <summary>
        /// Gets the recommendation by collab filtering.
        /// </summary>
        /// <param name="watchedMovies">The watched movies.</param>
        /// <returns></returns>
        Task<IEnumerable<int>> GetRecommendationByCollabFiltering(WatchedMoviesDto watchedMovies);

        /// <summary>
        /// Gets the recommendation by content filtering.
        /// </summary>
        /// <param name="movieId">The movie identifier.</param>
        /// <param name="numberOfElements">The number of elements.</param>
        /// <returns></returns>
        Task<IEnumerable<int>> GetRecommendationByContentFiltering(int movieId, int numberOfElements);
    }
}