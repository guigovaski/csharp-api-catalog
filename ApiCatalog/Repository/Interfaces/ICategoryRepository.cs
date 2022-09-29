using ApiCatalog.Models;
using ApiCatalog.Pagination;

namespace ApiCatalog.Repository.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<PagedList<Category>> GetCategoriesPaginated(CategoriesPageParameters obj);
    Task<IEnumerable<Category>> GetCategoriesProducts();
}
