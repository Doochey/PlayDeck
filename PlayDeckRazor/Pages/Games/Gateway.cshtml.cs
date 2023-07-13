using System.Collections;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using PlayDeckRazor.Data;
using PlayDeckRazor.Model;
using System;
using System.IO;
using System.Text;

namespace PlayDeckRazor.Pages.Games;

public class GatewayModel : PageModel
{
    private readonly PlayDeckRazor.Data.PlayDeckRazorContext _context;
    
    [BindProperty(SupportsGet = true)]
    public Game Game { get; set; } = default!;
    
    [BindProperty(SupportsGet = true)]
    public int? DeckID { get; set; }
    
    /// <summary>
    /// 0 = Unplayed, 1 = Complete
    /// </summary>
    [BindProperty]
    public int? PlayStatusChange { get; set; }
    [BindProperty]
    public int? RatingChange { get; set; }
    [BindProperty]
    public int? GameID { get; set; }
    
    [BindProperty]
    public bool ToggleFavourite { get; set; }
    
    [BindProperty]
    public string Fav_Toggle { get; set; }

    /// <summary>
    /// id for game to be deleted, submitted by post from _GameCard delete button
    /// </summary>
    [BindProperty]
    public int? GameDeleteId { get; set; }
    
    public GatewayModel(PlayDeckRazorContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Takes an ID of a game and returns all details of that game as JSON
    /// </summary>
    /// <param name="id">ID of Game in database</param>
    /// <returns></returns>
    public async Task<IActionResult> OnGetRetrieveAsync(int? id)
    {
        if (_context.Game == null)
        {
            return new NotFoundResult();
        }
        
        var game =  await _context.Game.FirstOrDefaultAsync(g => g.ID == id);
        if (game == null)
        {
            return new NotFoundResult();
        }
        Game = game;
        return new OkObjectResult(game.ToJson());
    }

    /// <summary>
    /// Saves all changes of the posted game if it exists in the database
    /// Returns HTML of a new Game Card with updated details.
    /// </summary>
    /// <returns>_GameCard HTML</returns>
    public async Task<IActionResult> OnPostEditAsync()
    {
        if (!ModelState.IsValid)
        {
            return new BadRequestResult();
        }

        if (GameExists(Game.ID))
        {
            if (Fav_Toggle.Equals("Favourite"))
            {
                Game.Favourite = true;
            }
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
    
    /// <summary>
    /// Saves all changes of the posted game if it exists in the database
    /// Returns HTML of a new Game Info Panel with updated details.
    /// </summary>
    /// <returns>_GameInfoPanel HTML</returns>
    public async Task<IActionResult> OnPostEditGameViewAsync()
    {
        if (!ModelState.IsValid)
        {
            return new BadRequestResult();
        }

        if (GameExists(Game.ID))
        {
            if (Fav_Toggle.Equals("Favourite"))
            {
                Game.Favourite = true;
            }
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

    /// <summary>
    /// Adds posted game to the database
    /// Returns HTML of new Game Card
    /// </summary>
    /// <returns>_GameCard HTML</returns>
    public async Task<IActionResult> OnPostAddAsync()
    {
        if (ModelState.IsValid && !GameExists(Game.ID))
        {
            if (Fav_Toggle.Equals("Favourite"))
            {
                Game.Favourite = true;
            }
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
    
    /// <summary>
    /// Finds game with posted ID and removes it from the database
    /// </summary>
    /// <returns>Posted Game ID</returns>
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

    /// <summary>
    /// Finds game by posted Game ID and updates its rating property
    /// Returns HTML of new Game Info Panel
    /// </summary>
    /// <returns>_GameInfoPanel HTML</returns>
    public async Task<IActionResult> OnPostRatingChangeAsync()
    {
        Game = await _context.Game.FindAsync(GameID);
        if (Game == null)
        {
            return new NotFoundResult();
        }
        
        if (RatingChange != null)
        {
            Game.Rating = RatingChange;
        } 
        else
        {
            return new BadRequestResult();
        }
        
        _context.Attach(Game).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();

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

    /// <summary>
    /// Finds game by posted Game ID and updates its PlayStatus property
    /// Returns new Game Info Panel HTML
    /// </summary>
    /// <returns>_GameInfoPanel HTML</returns>
    public async Task<IActionResult> OnPostPlayStatusChangeAsync()
    {
        Game = await _context.Game.FindAsync(GameID);
        if (Game == null)
        {
            return new NotFoundResult();
        }
        
        if (PlayStatusChange != null)
        {
            Game.PlayStatus = PlayStatusChange;
        }
        else
        {
            return new BadRequestResult();
        }
        
        _context.Attach(Game).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();

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
    
    /// <summary>
    /// Finds game by posted Game ID and updates its Favourite property
    /// Returns new Game Info Panel HTML
    /// </summary>
    /// <returns>_GameInfoPanel HTML</returns>
    public async Task<IActionResult> OnPostToggleFavGameViewAsync()
    {
        Game = await _context.Game.FindAsync(GameID);
        if (Game == null)
        {
            return new NotFoundResult();
        }

        if (ToggleFavourite)
        {
            Game.Favourite = !Game.Favourite;  
        }
        else
        {
            return new BadRequestResult();
        }
        
        
        _context.Attach(Game).State = EntityState.Modified;
        
        await _context.SaveChangesAsync();

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

    /// <summary>
    /// Outputs details of every game in the database to a CSV file
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> OnPostExportAsync()
    {
        List<object> games = (from Game in _context.Game
            select new object[] {
                Game.CompleteDate,
                Game.DeckID,
                Game.Favourite,
                Game.ID,
                Game.ImageURL,
                Game.LastPlayed,
                Game.Platform,
                Game.PlayStatus,
                Game.PlayTime,
                Game.Rating,
                Game.StartDate,
                Game.Title
            }).ToList<object>();
        //Insert the Column Names.
        games.Insert(0, new string[12] { "CompleteDate", "DeckID", "Favourite", "ID", "ImageURL", 
            "LastPlayed", "Platform", "PlayStatus", "PlayTime", "Rating", "StartDate", "Title" });
        
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < games.Count; i++)
        {
            string[] game = Array.ConvertAll((object[])games[i], Convert.ToString)!;
            for (int j = 0; j < game.Length; j++)
            {
                //Append data with separator.
                sb.Append(game[j].ToString() + ',');
            }
 
            //Append new line character.
            sb.Append("\r\n");
 
        }

        var systemPath = System.Environment.
            GetFolderPath(
                Environment.SpecialFolder.CommonApplicationData
            );
        var path = systemPath + "\\PlayDeck";
        Directory.CreateDirectory(path);
        var complete = Path.Combine(path , "PlayDeckExport.csv");
        
        // Create the file, or overwrite if the file exists.
        await using (FileStream fs = System.IO.File.Create(complete))
        {
            byte[] info = Encoding.UTF8.GetBytes(sb.ToString());
            // Add some information to the file.
            fs.Write(info, 0, info.Length);
        }
 
        return new OkObjectResult(1);
    }

    /// <summary>
    /// Imports all game data from CSV File, Does not preserve IDs, all games are added as new
    /// Will likely cause duplicates if used on a non-empty database
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> OnPostImportAsync()
    {
        var systemPath = System.Environment.
            GetFolderPath(
                Environment.SpecialFolder.CommonApplicationData
            );
        var path = systemPath + "\\PlayDeck";
        Directory.CreateDirectory(path);
        var complete = Path.Combine(path , "PlayDeckExport.csv");
        var readcsv = System.IO.File.ReadAllText(complete);
        string[] csvfilerecord = readcsv.Split('\n');

        foreach (var row in csvfilerecord)
        {
            if (!string.IsNullOrEmpty(row))
            {
                
                var cells = row.Split(',');
                if (!cells[0].Equals("CompleteDate"))
                {
                    var game = new Game();
                    {
                        if (cells[0] != "")
                        {
                            game.CompleteDate = DateTime.Parse(cells[0]);
                        }
                        if (cells[1] != "")
                        {
                            game.DeckID = Int32.Parse(cells[1]);
                        }
                        game.Favourite = bool.Parse(cells[2].ToLower());
                        game.ID =  0;
                        game.ImageURL = cells[4];
                        if (cells[5] != "")
                        {
                            game.LastPlayed = DateTime.Parse(cells[5]);
                        }
                        game.Platform = null;
                        game.PlayStatus = Int32.Parse(cells[7]);
                        if (cells[8] != "")
                        {
                            game.PlayTime = Single.Parse(cells[8]);
                        }
                        if (cells[9] != "")
                        {
                            game.Rating = Int32.Parse(cells[9]);
                        }
                        if (cells[10] != "")
                        {
                            game.StartDate = DateTime.Parse(cells[10]);
                        }
                        game.Title = cells[11];
                    };
                    _context.Game.Add(game);
                }
            }
        }
        await _context.SaveChangesAsync();
        return new OkObjectResult(1);
    }

}