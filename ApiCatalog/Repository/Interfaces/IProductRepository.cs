using ApiCatalog.Models;
using ApiCatalog.Pagination;

namespace ApiCatalog.Repository.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<PagedList<Product>> GetProductsPaginated(ProductsPageParameters obj);
    Task<IEnumerable<Product>> GetProductsByPrice(double maxPrice);
}
