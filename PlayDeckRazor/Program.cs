using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PlayDeckRazor.Data;
var builder = WebApplication.CreateBuilder(args);

// Create dir for db
Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/App_Data");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<PlayDeckRazorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PlayDeckRazorContext") 
                         // Allows local db to be stored in app dir, otherwise default is \Users\{username}
                         .Replace("[DataDirectory]", Directory.GetCurrentDirectory())
                         ?? throw new InvalidOperationException("Connection string 'PlayDeckRazorContext' not found.")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<PlayDeckRazorContext>();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Urls.Add("https://localhost:6610");

app.Run();

