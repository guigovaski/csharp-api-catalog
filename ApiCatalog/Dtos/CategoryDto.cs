using ApiCatalog.Models;

namespace ApiCatalog.Dtos;

public class CategoryDto
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public string? ImageUrl { get; set; }
    public ICollection<Product>? Products { get; set; }
}
