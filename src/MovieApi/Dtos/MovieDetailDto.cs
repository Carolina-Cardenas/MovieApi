using MovieApi.Models;

namespace MovieApi.Dtos
{
    public class MovieDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int Duration { get; set; }

        public MovieDetails? Details { get; set; }
        public List<ReviewDto> Reviews { get; set; } = new List<ReviewDto>();
        public List<ActorDto> Actors { get; set; } = new List<ActorDto>();
    }
}