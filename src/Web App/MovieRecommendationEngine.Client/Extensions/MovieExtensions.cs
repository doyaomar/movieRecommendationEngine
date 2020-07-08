using MovieRecommendationEngine.Client.Models;

namespace MovieRecommendationEngine.Client.Extensions
{
    public static class MovieExtensions
    {
        public static Movie ToMovie(this MovieDto item, string imageUrl)
        {
            if (item is null)
            {
                return null;
            }

            return new Movie
            {
                posterUri = $"{imageUrl}/t/p/w500{item.PosterPath ?? item.BackdropPath ?? string.Empty}",
                Title = item.Title,
                Overview = item.Overview
            };
        }
    }
}
