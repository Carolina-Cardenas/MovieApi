using Microsoft.EntityFrameworkCore;
using MovieApi.Models;

namespace MovieApi
{
    public class MovieContext : DbContext
    {
        public MovieContext(DbContextOptions<MovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieDetails> MovieDetails { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public DbSet<Actor> Actors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>()
                .HasOne(movie => movie.Details)
                .WithOne()
                .HasForeignKey<MovieDetails>(details => details.MovieId);

            modelBuilder.Entity<Movie>()
                .HasMany(movie => movie.Reviews)
                .WithOne()
                .HasForeignKey(review => review.MovieId);

           modelBuilder.Entity<Movie>()
                .HasMany(movie => movie.Actors)
                .WithMany(actor => actor.Movies)
                .UsingEntity(
        "MovieActor",
                join => join.HasOne(typeof(Actor))
                .WithMany()
                .HasForeignKey("ActorId"),
                 join => join.HasOne(typeof(Movie))
                .WithMany()
                .HasForeignKey("MovieId"));
        }
    }
}