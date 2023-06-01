
namespace PlayDeckRazor.Model;

public class Deck : IDeck
{
    public int ID { get; set; }
    public string Title { get; set; }
    
    public List<Game> GameList { get; set; }


    public Deck(int id)
    {
        GameList = new List<Game>();
        ID = id;
        Title = "Deck: " + ID;
    }
}