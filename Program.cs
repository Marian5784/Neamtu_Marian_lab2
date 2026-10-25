using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("Neamtu_Marian_lab2Context")
    ?? throw new InvalidOperationException(
        "Connection string 'Neamtu_Marian_lab2Context' not found.");

// Configurarea bazei de date
builder.Services.AddDbContext<Neamtu_Marian_lab2Context>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Servicii pentru Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configurarea aplicatiei
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();