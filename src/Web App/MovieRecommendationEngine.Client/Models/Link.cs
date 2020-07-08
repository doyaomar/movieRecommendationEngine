namespace MovieRecommendationEngine.Client.Models
{
    public partial class Link
    {
        public int MovieId { get; set; }
        public int ImdbId { get; set; }
        public int? TmdbId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
