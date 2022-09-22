using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalog.Models;

public class Category
{
    public int CategoryId { get; set; }

    [Required, StringLength(255)]
    public string? Name { get; set; }

    [Required, StringLength(255)]
    public string? ImageUrl { get; set; }
    public ICollection<Product>? Products { get; set; } = new Collection<Product>();
}
