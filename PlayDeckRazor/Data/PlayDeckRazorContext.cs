using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlayDeckRazor.Model;

namespace PlayDeckRazor.Data
{
    //TODO: Add Deck table and rewrite hard coded deck allocations
    //TODO: Add Platforms table to allow for sorting by platform
    public class PlayDeckRazorContext : DbContext
    {
        public PlayDeckRazorContext (DbContextOptions<PlayDeckRazorContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Game { get; set; } = default!;
    }
}
