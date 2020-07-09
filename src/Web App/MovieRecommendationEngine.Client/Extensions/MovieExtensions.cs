using MovieRecommendationEngine.Client.Models;

namespace MovieRecommendationEngine.Client.Extensions
{
    /// <summary>
    /// MovieExtensions
    /// </summary>
    public static class MovieExtensions
    {
        /// <summary>
        /// Converts to movie.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="imageUrl">The image URL.</param>
        /// <returns></returns>
        public static Movie ToMovie(this MovieTmdbDto item, string imageUrl)
        {
            if (item is null)
            {
                return null;
            }

            return new Movie
            {
                PosterUri = $"{imageUrl}/t/p/w500{item.PosterPath ?? item.BackdropPath ?? string.Empty}",
                Title = item.Title,
                Overview = item.Overview
            };
        }
    }
}
