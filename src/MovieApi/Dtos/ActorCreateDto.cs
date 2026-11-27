using System.ComponentModel.DataAnnotations;


namespace MovieApi.Dtos
{
    public class ActorCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public int BirthYear { get; set; }
    }
}