using ApiCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Data;

public class ApiCatalogContext : DbContext
{
    public ApiCatalogContext(DbContextOptions<ApiCatalogContext> options) : base(options)
    {
    }

    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();
    }
}
