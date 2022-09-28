using ApiCatalog.Data;
using ApiCatalog.Models;
using ApiCatalog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApiCatalogContext context) : base(context)
    {
    }

    public IEnumerable<Category> GetCategoriesProducts()
    {
        return Get().Include(c => c.Products).AsNoTracking();
    }
}
