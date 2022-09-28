using ApiCatalog.Models;

namespace ApiCatalog.Repository.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    IEnumerable<Category> GetCategoriesProducts();
}
