using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PlayDeckRazor.Model;

namespace PlayDeckRazor.Pages.Shared;

public class _ModalGameDetailsModel : PageModel
{
    public Game Game { get; set; }
    
    /// <summary>
    /// User friendly display of different decks which are otherwise represented as ints
    /// </summary>
    public List<SelectListItem> Decks { get; set; }
    /// <summary>
    /// User friendly display of different PlayStatus which are otherwise represented as ints
    /// </summary>
    public List<SelectListItem> Statuses { get; set; }
    
    
    public _ModalGameDetailsModel(PlayDeckRazor.Data.PlayDeckRazorContext context)
    {
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
}