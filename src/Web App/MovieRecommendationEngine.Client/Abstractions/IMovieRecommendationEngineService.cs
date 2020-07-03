using MovieRecommendationEngine.Client.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Abstractions
{
    public interface IMovieRecommendationEngineService
    {
        Task<IEnumerable<int>> GetRecommendationByCollabFiltering(List<RatingDto> watchedMovies);

        Task<IEnumerable<int>> GetRecommendationByContentFiltering(int movieId, int numberOfElements);
    }
}