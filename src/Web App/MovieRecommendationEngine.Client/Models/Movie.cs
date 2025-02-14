﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRecommendationEngine.Client.Models
{
    /// <summary>
    /// Movie
    /// </summary>
    public partial class Movie
    {
        /// <summary>
        /// Gets or sets the movie identifier.
        /// </summary>
        /// <value>
        /// The movie identifier.
        /// </value>
        public int MovieId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the genres.
        /// </summary>
        /// <value>
        /// The genres.
        /// </value>
        public string Genres { get; set; }

        /// <summary>
        /// Gets or sets the poster URI.
        /// </summary>
        /// <value>
        /// The poster URI.
        /// </value>
        [NotMapped]
        public string PosterUri { get; set; }

        /// <summary>
        /// Gets or sets the overview.
        /// </summary>
        /// <value>
        /// The overview.
        /// </value>
        [NotMapped]
        public string Overview { get; set; }
    }
}
