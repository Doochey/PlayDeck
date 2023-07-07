using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PlayDeckRazor.Data;
using PlayDeckRazor.Model;

namespace PlayDeckRazor.Pages.Games;

public class GatewayModel : PageModel
{
    private readonly PlayDeckRazor.Data.PlayDeckRazorContext _context;
    
    [BindProperty(SupportsGet = true)]
    public Game Game { get; set; } = default!;
    
    [BindProperty(SupportsGet = true)]
    public int? DeckID { get; set; }

    public GatewayModel(PlayDeckRazorContext context)
    {
        _context = context;
    }
    
    public async Task<IActionResult> OnGetRetrieveAsync(int? id)
    {
        if (_context.Game == null)
        {
            return NotFound();
        }

        // User editing existing entry
        var game =  await _context.Game.FirstOrDefaultAsync(g => g.ID == id);
        if (game == null)
        {
            return NotFound();
        }
        Game = game;
        return new OkObjectResult(game.ToJson());
    }

    public async Task<IActionResult> OnPostEditAsync()
    {
        if (!ModelState.IsValid)
        {
            return new BadRequestResult();
        }

        if (GameExists(Game.ID))
        {
            _context.Attach(Game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(Game.ID))
                {
                    return new NotFoundResult();
                }
                else
                {
                    throw;
                }
            }
            ViewData["GameCard"] = Game;
            var model = new IndexModel(_context);
            var test = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "IndexModel", model } };
            test.Model = model;
            test["GameCard"] = Game;
            return new PartialViewResult()
            {
                ViewName = "_GameCard",
                ViewData = test,
            };
        }
        return new BadRequestResult();
    }
    
    private bool GameExists(int id)
    {
        return (_context.Game?.Any(e => e.ID == id)).GetValueOrDefault();
    }
    
        
}