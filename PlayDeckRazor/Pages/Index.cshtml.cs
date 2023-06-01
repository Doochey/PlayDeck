using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayDeckRazor.Data;
using PlayDeckRazor.Model;

namespace PlayDeckRazor.Pages;

public class IndexModel : PageModel
{
    private readonly PlayDeckRazorContext _context;

    /// <summary>
    /// List of Decks containing all Games in the DB, Max 10 decks, 0 = Unassigned, 1 = On deck, 2 = completed
    /// 3 -> 10 for user allocation
    /// </summary>
    public List<Deck> Decks = new List<Deck>(10);
    
    [BindProperty(SupportsGet = true)]
    public string? CollectionSearch { get; set; }
    
    public List<Game>? FilteredList { get; set; }
    
    public SelectList? Platform { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? PlatformSearch { get; set; }
    
    [BindProperty]
    public int? gameDeleteID { get; set; }
    
    public IndexModel(PlayDeckRazorContext context)
    {
        _context = context;

        for (int i = 0; i < Decks.Capacity; i++)
        {
            Decks.Add(new Deck(i));
        }

        Decks[0].Title = "My Collection";
        Decks[1].Title = "Currently Playing";
        Decks[2].Title = "Complete";
    }

    public async Task OnGetAsync()
    {
        await foreach (Game g in _context.Game)
        {
            Decks[g.DeckID].GameList.Add(g);
        }
        
        
        if (!string.IsNullOrEmpty(CollectionSearch))
        {
            var games = 
                from g in _context.Game
                select g;
            games = games.Where(s => s.Title.Contains(CollectionSearch) && s.DeckID == 0);
            FilteredList = await games.ToListAsync();
        }

        
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (gameDeleteID != null)
        {
            var game = await _context.Game.FindAsync(gameDeleteID);
            if (game != null)
            {
                _context.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
        
        return RedirectToPage("./Index");
    }
}