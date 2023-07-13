using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayDeckRazor.Data;
using PlayDeckRazor.Migrations;
using PlayDeckRazor.Model;

namespace PlayDeckRazor.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly PlayDeckRazor.Data.PlayDeckRazorContext _context;
        
        
        [BindProperty]
        public Game Game { get; set; } = default!;

        [BindProperty(SupportsGet = true)] 
        public int? DeckID { get; set; }
        
        /// <summary>
        /// Header displayed above form, changes depending on value of EditMode
        /// </summary>
        public string PageHeader { get; set; }
        
        /// <summary>
        /// true if user is editing existing entry
        /// </summary>
        public bool EditMode { get; set; }
        
        /// <summary>
        /// User friendly display of different decks which are otherwise represented as ints
        /// </summary>
        public List<SelectListItem> Decks { get; set; }
        /// <summary>
        /// User friendly display of different PlayStatus which are otherwise represented as ints
        /// </summary>
        public List<SelectListItem> Statuses { get; set; }

        public EditModel(PlayDeckRazor.Data.PlayDeckRazorContext context)
        {
            _context = context;
            
            // TODO: Replace with db query when db is updated to store decks
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
                },
                new()
                {
                    Text = "Wishlist",
                    Value = "3"
                }
            };
            
            Statuses = new List<SelectListItem>
            {
                new()
                {
                    Text = "Unfinished",
                    Value = "0"
                },
                new()
                {
                    Text = "Complete",
                    Value = "1"
                }
            };
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (_context.Game == null)
            {
                return NotFound();
            }

            // If no route value provided
            if (id == null)
            {
                // User adding a new entry
                EditMode = false;
                PageHeader = "Add A New Game";
                Game = new Game();
                DeckID ??= 0;
                
                // Set play status default depending on which deck '+' button user pressed
                if (DeckID == 2)
                {
                    Game.PlayStatus = 1;
                }
                else
                {
                    Game.PlayStatus = 0;
                }
                Game.DeckID = (int)DeckID;
            }
            else
            {
                // User editing existing entry
                EditMode = true;
                var game =  await _context.Game.FirstOrDefaultAsync(g => g.ID == id);
                if (game == null)
                {
                    return NotFound();
                }
                Game = game;
                PageHeader = "Edit " + Game.Title + " Details";
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
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
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                _context.Game.Add(Game);
                await _context.SaveChangesAsync();
            }
            // Send user to detailed view of their entry
            return RedirectToPage("/GameView", new
            {
                id = Game.ID
            });
        }

        private bool GameExists(int id)
        {
          return (_context.Game?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
