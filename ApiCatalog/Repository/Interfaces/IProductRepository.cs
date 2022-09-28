using ApiCatalog.Models;

namespace ApiCatalog.Repository.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    IEnumerable<Product> GetProductsByPrice(double maxPrice);
}
