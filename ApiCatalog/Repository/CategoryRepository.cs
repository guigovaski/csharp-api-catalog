using ApiCatalog.Data;
using ApiCatalog.Models;
using ApiCatalog.Pagination;
using ApiCatalog.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Repository;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(ApiCatalogContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Category>> GetCategoriesProducts()
    {
        return await Get().Include(c => c.Products).AsNoTracking().ToListAsync();
    }

    public async Task<PagedList<Category>> GetCategoriesPaginated(CategoriesPageParameters obj)
    {
        return await PagedList<Category>.ToPagedList(Get(), obj.CurrentPage, obj.PageSize);
    }
}
