using MovieApi.Models;

namespace MovieApi.Extensions
{
    public static class SeedDataExtensions
    {
        public static void SeedData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<MovieContext>();

            if (context.Movies.Any(movie => movie.Title == "Inception" && movie.Year == 2010))
            {
                return;
            }

            var movie = new Movie
            {
                Title = "Inception",
                Year = 2010,
                Genre = "Science fiction",
                Duration = 148,
                Details = new MovieDetails
                {
                    synopsis = "A thief enters people's dreams to steal secrets.",
                    Language = "English",
                    budget = 160_000_000m
                },
                Reviews = new List<Review>
                {
                    new Review
                    {
                        ReviewerName = "Alex",
                        Comment = "An original story with memorable visuals.",
                        Rating = 5
                    }
                },
                Actors = new List<Actor>
                {
                    new Actor
                    {
                        Name = "Leonardo DiCaprio",
                        BirthYear = 1974
                    }
                }
            };

            context.Movies.Add(movie);
            context.SaveChanges();
        }
    }
}