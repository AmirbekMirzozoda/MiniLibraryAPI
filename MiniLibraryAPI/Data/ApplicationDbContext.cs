using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Book>()
            .HasIndex(b => b.PublishedDate);
        
        modelBuilder
            .Entity<Book>()
            .HasIndex(x => new { x.Name, x.AuthorId, x.CategoryId })
            .IsUnique();
            
        base.OnModelCreating(modelBuilder);
    }
}