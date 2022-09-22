using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ApiCatalog.Models;

public class Product
{
    public int ProductId { get; set; }

    [Required, StringLength(255)]
    public string? Name { get; set; }

    [Required, StringLength(255)]
    public string? Description { get; set; }

    [Required]
    public double Price { get; set; }

    [Required, StringLength(255)]
    public string? ImageUrl { get; set; }
    public int Inventory { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }
}
