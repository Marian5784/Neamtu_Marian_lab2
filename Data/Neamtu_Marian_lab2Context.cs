using Microsoft.EntityFrameworkCore;

public class Neamtu_Marian_lab2Context(
    DbContextOptions<Neamtu_Marian_lab2Context> options
) : DbContext(options)
{
    public DbSet<Neamtu_Marian_lab2.Models.Book> Book { get; set; } = default!;

    public DbSet<Neamtu_Marian_lab2.Models.Publisher> Publisher { get; set; } = default!;

    public DbSet<Neamtu_Marian_lab2.Models.Authors> Authors { get; set; } = default!;
}