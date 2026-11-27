using System.ComponentModel.DataAnnotations;

namespace MovieApi.Dtos
{
    public class MovieCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        public int Year { get; set; }

        [Required]
        public string Genre { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Duration { get; set; }
    }
}