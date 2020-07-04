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

        public virtual DbSet<Link> Links { get; set; }

        public virtual DbSet<Movie> Movies { get; set; }

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
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("Movies");

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

                //entity.HasMany(m => m.Links)
                //    .WithOne(m => m.Movie)
                //    .HasForeignKey(d => d.MovieId)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("FK_Links_Movies");

                //entity
                //.UsePropertyAccessMode(PropertyAccessMode.Property);
            });

            modelBuilder.Entity<Link>(entity =>
            {
                entity.ToTable("Links");

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
