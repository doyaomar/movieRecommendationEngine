using MovieRecommendationEngine.Client.Models;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Abstractions
{
    public interface ITheMovieDBService
    {
        Task<MovieDto> GetMovieById(int tmdbId);
    }
}
