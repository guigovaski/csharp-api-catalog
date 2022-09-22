using Microsoft.EntityFrameworkCore;
using ApiCatalog.Models;

namespace ApiCatalog.Data;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Comidas", ImageUrl = "comidas.jpg" },
            new Category { CategoryId = 2, Name = "Bebidas", ImageUrl = "bebidas.jpg" },
            new Category { CategoryId = 3, Name = "Sobremesas", ImageUrl = "sobremesas.jpg" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { ProductId = 1, Name = "Lasanha", Description = "Lasanha congelada", Price = 15.00, Date = DateTime.Now, ImageUrl = "lasanha.jpg", Inventory = 25, CategoryId = 1 },
            new Product { ProductId = 2, Name = "Guaraná", Description = "Refrigerante Guaraná 2L", Price = 7.50, Date = DateTime.Now, ImageUrl = "guarana.jpg", Inventory = 54, CategoryId = 2 },
            new Product { ProductId = 3, Name = "Petit Gateau", Description = "Sobremesa deliciosa. Acompanha sorvete", Price = 20.00, Date = DateTime.Now, ImageUrl = "petit-gateau.jpg", Inventory = 17, CategoryId = 3 }
        );
    }
}
