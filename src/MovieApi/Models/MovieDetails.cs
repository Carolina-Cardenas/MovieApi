namespace MovieApi.Models
{
    public class MovieDetails
    {
       public int ID{get; set;}
       public string synopsis {get; set;} = string.Empty;
        
       public string Lenguage {get; set;} = string.Empty;

       public decimal budget {get; set;}

       public int MovieId {get; set;}


    }
}