using Microsoft.EntityFrameworkCore;
using MovieRecommendationEngine.Client.Models;

namespace MovieRecommendationEngine.Client.Infrastructure
{
    public partial class MovieLensContext : DbContext
    {
        public MovieLensContext()
        {
        }

        public MovieLensContext(DbContextOptions<MovieLensContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Links> Links { get; set; }
        public virtual DbSet<Movies> Movies { get; set; }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("server=.; Trusted_Connection=True; Database=MovieLens");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Links>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ImdbId).HasColumnName("imdbId");

                entity.Property(e => e.MovieId).HasColumnName("movieId");

                entity.Property(e => e.TmdbId).HasColumnName("tmdbId");

                entity.HasOne(d => d.Movie)
                    .WithMany()
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Links_Movies");
            });

            modelBuilder.Entity<Movies>(entity =>
            {
                entity.HasKey(e => e.MovieId);

                entity.Property(e => e.MovieId)
                    .HasColumnName("movieId")
                    .ValueGeneratedNever();

                entity.Property(e => e.Genres)
                    .HasColumnName("genres")
                    .HasMaxLength(200);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
