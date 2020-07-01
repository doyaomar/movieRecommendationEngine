using MovieRecommendationEngine.Client.Abstractions;
using System;
using System.Collections.Generic;
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
    }
}
