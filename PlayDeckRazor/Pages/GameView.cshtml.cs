using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayDeckRazor.Data;
using PlayDeckRazor.Model;

namespace PlayDeckRazor.Pages;

public class GameViewModel : PageModel
{
    
    private readonly PlayDeckRazorContext _context;
    
    
    public Game? game { get; set; }
    
    [BindProperty]
    public int? ratingChange { get; set; }
    [BindProperty]
    public int? playStatusChange { get; set; }
    [BindProperty]
    public int gameID { get; set; }
    
    [BindProperty]
    public int? gameDeleteID { get; set; }
    
    [StringLength(50, MinimumLength = 1), Display(Name = "Title")]
    [BindProperty]
    public string? titleChange { get; set; }
    
    
    public List<SelectListItem> Decks { get; set; }

    public GameViewModel(PlayDeckRazorContext context)
    {
        _context = context;
        
        Decks = new List<SelectListItem>
        {
            new()
            {
                Text = "My Collection",
                Value = "0"
            },
            new()
            {
                Text = "Currently Playing",
                Value = "1"
            },
            new()
            {
                Text = "Complete",
                Value = "2"
            }
        };
    }
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        game = await _context.Game.FindAsync(id);
        if (game == null)
        {
            return NotFound();
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (gameDeleteID != null)
        {
            game = await _context.Game.FindAsync(gameDeleteID);
            if (game == null)
            {
                return NotFound();
            }
            _context.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
        
        game = await _context.Game.FindAsync(gameID);
        if (game == null)
        {
            return NotFound();
        }
        
        if (ratingChange != null)
        {
            game.Rating = ratingChange;
        }

        if (playStatusChange != null)
        {
            game.PlayStatus = playStatusChange;
        }
        
        if (titleChange != null)
        {
            game.Title = titleChange;
        }
        
        _context.Attach(game).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();

        return Page();
    }
}