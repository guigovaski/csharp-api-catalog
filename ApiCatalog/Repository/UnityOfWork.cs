using ApiCatalog.Data;
using ApiCatalog.Repository.Interfaces;

namespace ApiCatalog.Repository;

public class UnityOfWork : IUnityOfWork
{
    private ProductRepository? _productRepository;
    private CategoryRepository? _categoryRepository;
    private ApiCatalogContext _context;

    public IProductRepository ProductRepository
    {
        get
        {
            return _productRepository ?? new ProductRepository(_context);
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            return _categoryRepository ?? new CategoryRepository(_context);
        }
    }

    public UnityOfWork(ApiCatalogContext context)
    {
        _context = context;
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
}
