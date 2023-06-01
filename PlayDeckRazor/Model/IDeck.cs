using Microsoft.EntityFrameworkCore;

namespace PlayDeckRazor.Model;

public interface IDeck
{
    public int ID { get; set; }
    public string Title { get; set; }
    
    /// <summary>
    /// List of games where game deck id = this deck id
    /// </summary>
    public List<Game> GameList { get; set; }
}