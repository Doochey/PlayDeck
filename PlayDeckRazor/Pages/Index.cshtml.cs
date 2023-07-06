using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlayDeckRazor.Data;
using PlayDeckRazor.Model;

namespace PlayDeckRazor.Pages;

public class IndexModel : PageModel
{
    public readonly PlayDeckRazorContext _context;

    /// <summary>
    /// List of Decks containing all Games in the DB, Max 10 decks, 0 = Unassigned, 1 = On deck, 2 = completed
    /// 3 -> 10 for user allocation
    /// </summary>
    public List<Deck> Decks = new List<Deck>(10);
    
    public List<Game>? FilteredList { get; set; }
    
    /* Not Implemented yet
    public SelectList? Platform { get; set; }
    [BindProperty(SupportsGet = true)]
    public string? PlatformSearch { get; set; }
    */
    
    /// <summary>
    /// id for game to be deleted, submitted by post from _GameCard delete button
    /// </summary>
    [BindProperty]
    public int? GameDeleteId { get; set; }
    
    public IndexModel(PlayDeckRazorContext context)
    {
        _context = context;
        
        // TODO: Replace with db query when Deck storage is implemented
        for (int i = 0; i < Decks.Capacity; i++)
        {
            Decks.Add(new Deck(i));
        }
        Decks[0].Title = "My Collection";
        Decks[1].Title = "Currently Playing";
        Decks[2].Title = "Complete";
        Decks[3].Title = "Wishlist";
    }

    public async Task OnGetAsync()
    {
        // Add each game to respective deck
        await foreach (Game g in _context.Game)
        {
            Decks[g.DeckID].GameList.Add(g);
        }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        /*
        if (GameDeleteId != null)
        {
            // Ensure game exists and delete
            var game = await _context.Game.FindAsync(GameDeleteId);
            if (game != null)
            {
                _context.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
        */
        return RedirectToPage("./Index");
    }

    public async Task<IActionResult> OnPostDeleteAsync()
    {
        
        if (GameDeleteId != null)
        {
            // Ensure game exists and delete
            var game = await _context.Game.FindAsync(GameDeleteId);
            if (game != null)
            {
                _context.Remove(game);
                await _context.SaveChangesAsync();
                return new OkObjectResult(GameDeleteId);
            }
            
            return new NotFoundResult();
            
        }
        return new BadRequestResult();
    }
}