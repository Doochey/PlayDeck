using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PlayDeckRazor.Model;

public class Game : IGame
{
    public int ID { get; set; }
    [Display(Name = "Deck (0 = unassigned, 1 = On Deck, 2 = Complete)"), Range(0, 10)]
    public int DeckID { get; set; }
    [StringLength(50, MinimumLength = 1)]
    public string Title { get; set; }
    [Display(Name = "Cover Image URL"), StringLength(500), Url]
    public string? ImageURL { get; set; }
    public Platform? Platform { get; set; }
    [Display(Name = "Play Status"), Range(0, 2)]
    public int? PlayStatus { get; set; }
    [Display(Name = "Start Date"), DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }
    [Display(Name = "Complete Date"), DataType(DataType.Date)]
    public DateTime? CompleteDate { get; set; }
    [Range(0, 10)]
    public int? Rating { get; set; }
    [Display(Name = "Play Time"), Range(0, 100000, ErrorMessage = "Only positive play time between 0 and 100000 allowed")]
    public float? PlayTime { get; set; }
    [Display(Name = "Last Played"), DataType(DataType.Date)]
    public DateTime? LastPlayed { get; set; }
}