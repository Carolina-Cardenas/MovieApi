namespace MovieApi.Models

{
  public class Movie

    {
        public int ID {get; set;}
        public string Title { get; set; } = string.Empty;
 
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;

        public int Duration { get; set; }

        public MovieDetails? Details { get; set; }

        public List<Review> Reviews { get; set; } = new List<Review>();

        public List<Actor> Actors { get; set; } = new List<Actor>();

    }
    }