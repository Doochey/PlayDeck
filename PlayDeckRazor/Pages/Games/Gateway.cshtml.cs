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
    
    /// <summary>
    /// id for game to be deleted, submitted by post from _GameCard delete button
    /// </summary>
    [BindProperty]
    public int? GameDeleteId { get; set; }
    
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
            IndexModel newPartialModel = new IndexModel(_context);
            ViewDataDictionary newViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "IndexModel", newPartialModel } };
            newViewData.Model = newPartialModel;
            newViewData["GameCard"] = Game;
            return new PartialViewResult()
            {
                ViewName = "_GameCard",
                ViewData = newViewData,
            };
        }
        return new BadRequestResult();
    }
    
    public async Task<IActionResult> OnPostEditGameViewAsync()
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
            GameViewModel newPartialModel = new GameViewModel(_context);
            ViewDataDictionary newViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "GameViewModel", newPartialModel } };
            newViewData.Model = newPartialModel;
            newViewData["GameView"] = Game;
            return new PartialViewResult()
            {
                ViewName = "_GameInfoPanel",
                ViewData = newViewData,
            };
        }
        return new BadRequestResult();
    }

    public async Task<IActionResult> OnPostAddAsync()
    {
        if (ModelState.IsValid && !GameExists(Game.ID))
        {
            _context.Game.Add(Game);
            await _context.SaveChangesAsync();

            IndexModel newPartialModel = new IndexModel(_context);
            ViewDataDictionary newViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary()) { { "IndexModel", newPartialModel } };
            newViewData.Model = newPartialModel;
            newViewData["GameCard"] = Game;
            return new PartialViewResult()
            {
                ViewName = "_GameCard",
                ViewData = newViewData,
            };
        }
        else
        {
            return new BadRequestResult();
        }
        
    }

    private bool GameExists(int id)
    {
        return (_context.Game?.Any(e => e.ID == id)).GetValueOrDefault();
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