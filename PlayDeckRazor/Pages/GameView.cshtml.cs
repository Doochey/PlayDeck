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
    
    public readonly PlayDeckRazorContext _context;
    
    
    public Game? Game { get; set; }
    
    [BindProperty]
    public int? RatingChange { get; set; }
    [BindProperty]
    public int? PlayStatusChange { get; set; }
    [BindProperty]
    public int GameID { get; set; }
    
    [BindProperty]
    public int? GameDeleteID { get; set; }
    
    public bool ToggleFavourite { get; set; }
    
    [StringLength(50, MinimumLength = 1), Display(Name = "Title")]
    [BindProperty]
    public string? TitleChange { get; set; }
    
    public GameViewModel(PlayDeckRazorContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Game = await _context.Game.FindAsync(id);
        if (Game == null)
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

        if (GameDeleteID != null)
        {
            // Ensure game exists and delete
            Game = await _context.Game.FindAsync(GameDeleteID);
            if (Game == null)
            {
                return NotFound();
            }
            _context.Remove(Game);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Index");
        }
        
        Game = await _context.Game.FindAsync(GameID);
        if (Game == null)
        {
            return NotFound();
        }
        
        if (RatingChange != null)
        {
            Game.Rating = RatingChange;
        }

        if (PlayStatusChange != null)
        {
            Game.PlayStatus = PlayStatusChange;
        }
        
        if (TitleChange != null)
        {
            Game.Title = TitleChange;
        }
        
        _context.Attach(Game).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();

        return Page();
    }
}