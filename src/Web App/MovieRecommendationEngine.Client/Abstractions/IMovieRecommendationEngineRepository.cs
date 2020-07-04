using MovieRecommendationEngine.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Abstractions
{
    interface IMovieRecommendationEngineRepository
    {
        Task<Movie> GetMovieById(int movieId);

        Task<int> GetTmdbIdById(int moviedId);

        Task<IEnumerable<Movie>> GetMovieByTitle(string title);
    }
}
