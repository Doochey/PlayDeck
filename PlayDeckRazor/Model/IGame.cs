using System.ComponentModel.DataAnnotations;

namespace PlayDeckRazor.Model;

public interface IGame
{
    public int ID { get; set; }
    
    /// <summary>
    /// id of the collection ( Deck ) the game ( card ) should belong to
    /// </summary>
    public int DeckID { get; set; }
    public string Title { get; set; }
    /// <summary>
    /// Url of the image to be used when presented on a card
    /// </summary>
    public string? ImageURL { get; set; }
    
    /// <summary>
    /// Gaming platform game was assigned to
    /// </summary>
    public Platform? Platform { get; set; }
    /// <summary>
    /// 0 = Un-played, 1 = playing, 2 = complete,
    /// values of >2 are currently unrecognised but exist for future use e.g. 3 = 100% completion, 4 = Abandoned
    /// </summary>
    public int? PlayStatus { get; set; }
    /// <summary>
    /// Date Game was first played
    /// </summary>
    public DateTime? StartDate { get; set; }
    /// <summary>
    /// Date Game was 'finished'
    /// </summary>
    public DateTime? CompleteDate { get; set; }
    /// <summary>
    /// 'Star' Rating of a Game out of 10
    /// </summary>
    public int? Rating { get; set; }
    
    /// <summary>
    /// Time spent in game in hours, in increments of 0.5
    /// </summary>
    public float? PlayTime { get; set; }
    
    /// <summary>
    /// Date game was last played
    /// </summary>
    public DateTime? LastPlayed { get; set; }
}