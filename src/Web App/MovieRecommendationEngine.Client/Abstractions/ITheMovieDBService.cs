using MovieRecommendationEngine.Client.Models;
using System.Threading.Tasks;

namespace MovieRecommendationEngine.Client.Abstractions
{
    interface ITheMovieDBService
    {
        Task<MovieDto> GetMovieById(int tmdbId);
    }
}
