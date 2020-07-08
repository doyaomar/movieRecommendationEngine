using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRecommendationEngine.Client.Models
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }

        [NotMapped]
        public string posterUri { get; set; }

        [NotMapped]
        public string Overview { get; set; }

        //public List<Link> Links { get; set; }
    }
}
