namespace MovieApi.Models
{
    public class Actor
    {
    public int ID {get; set;}
    public string Name {get; set; }= string.Empty;

    public int BirthYear { get; set; }   

    public List<Movie> Movies { get; set; } = new List<Movie>();

  
}
}
