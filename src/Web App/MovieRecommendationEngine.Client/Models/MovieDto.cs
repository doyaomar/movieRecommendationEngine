using Newtonsoft.Json;

namespace MovieRecommendationEngine.Client.Models
{
    public class MovieDto
    {
        public string Title { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        public string Homepage { get; set; }
    }
}
