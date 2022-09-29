using ApiCatalog.Data;
using ApiCatalog.Models;
using ApiCatalog.Pagination;
using ApiCatalog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Repository;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(ApiCatalogContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsByPrice(double maxPrice)
    {
        return await Get().Where(p => p.Price <= maxPrice).ToListAsync();
    }

    public async Task<PagedList<Product>> GetProductsPaginated(ProductsPageParameters obj)
    {
        return await PagedList<Product>.ToPagedList(Get(), obj.CurrentPage, obj.PageSize);
    }
}
