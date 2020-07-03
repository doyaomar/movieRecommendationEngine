﻿using System.Collections.Generic;

namespace MovieRecommendationEngine.Client.Models
{
    public partial class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Genres { get; set; }

        public virtual ICollection<Link> Links { get; set; }
    }
}
