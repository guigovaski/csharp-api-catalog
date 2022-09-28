using ApiCatalog.Data;
using ApiCatalog.Models;
using ApiCatalog.Repository.Interfaces;

namespace ApiCatalog.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApiCatalogContext context) : base(context)
    {
    }

    public IEnumerable<Product> GetProductsByPrice(double maxPrice)
    {
        return Get().Where(p => p.Price <= maxPrice);
    }
}
