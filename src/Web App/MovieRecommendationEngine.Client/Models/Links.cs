namespace MovieRecommendationEngine.Client.Models
{
    public partial class Links
    {
        public int MovieId { get; set; }
        public int ImdbId { get; set; }
        public int? TmdbId { get; set; }

        public virtual Movies Movie { get; set; }
    }
}
