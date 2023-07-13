using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlayDeckRazor.Data;
using PlayDeckRazor.Model;

namespace PlayDeckRazor.Pages.Decks;

public class ViewModel : PageModel
{
    public readonly PlayDeckRazorContext _context;
    
    public Deck Deck { get; set; }
    
    /// <summary>
    /// Search string from user input
    /// </summary>
    [BindProperty(SupportsGet = true)]
    public string? CollectionSearch { get; set; }
    
    /// <summary>
    /// id of game to be deleted
    /// </summary>
    [BindProperty]
    public int? GameDeleteId { get; set; }
    
    
    public ViewModel(PlayDeckRazorContext context)
    {
        _context = context;
    }
    
    public async Task OnGetAsync(int id, string title)
    {
        // Initialise Deck
        Deck = new Deck(id)
        {
            Title = title
        };

        List<Game> result;
        if (id != 4)
        {
            // Select all games with this deck id
            var games = 
                from g in _context.Game
                select g;
            games = games.Where(s => s.DeckID == Deck.ID);
            result = await games.ToListAsync();
        }
        else
        {
            // Viewing Favourites deck, get all games tagged as favourite
            var games = 
                from g in _context.Game
                select g;
            games = games.Where(s => s.Favourite == true);
            result = await games.ToListAsync();
        }
        
            
        foreach (Game g in result)
        {
            Deck.GameList.Add(g);
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (GameDeleteId != null)
        {
            // Ensure game exists for id and delete
            var game = await _context.Game.FindAsync(GameDeleteId);
            if (game != null)
            {
                _context.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
        return RedirectToPage("/Decks/View");
    }
}