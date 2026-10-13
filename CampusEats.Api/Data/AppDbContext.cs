using CampusEats.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusEats.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MenuItem>(e =>
        {
            e.Property(m => m.Name).HasMaxLength(100).IsRequired();
            e.Property(m => m.Category).HasMaxLength(50);
            e.Property(m => m.Price).HasPrecision(10, 2);
        });

        // Seed data so the deployed API returns real rows after `dotnet ef database update`
        modelBuilder.Entity<MenuItem>().HasData(
            new MenuItem { Id = 1, Name = "Chicken Fried Rice", Category = "Rice", Price = 850m },
            new MenuItem { Id = 2, Name = "Vegetable Kottu", Category = "Kottu", Price = 750m },
            new MenuItem { Id = 3, Name = "Fish Bun", Category = "Snacks", Price = 120m },
            new MenuItem { Id = 4, Name = "Iced Milo", Category = "Drinks", Price = 300m },
            new MenuItem { Id = 5, Name = "Egg Roti", Category = "Snacks", Price = 200m }
        );
    }
}
