using Domain.Word;
using Framework.Ddd;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Peristence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Word> Words { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Event>();
    }
}
