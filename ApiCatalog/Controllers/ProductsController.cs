using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiCatalog.Data;
using ApiCatalog.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ApiCatalogContext _context;

    public ProductsController(ApiCatalogContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Product>> Get()
    {
        try
        {
            var products = _context.Products?.AsNoTracking().ToList();

            if (products is null)
            {
                return NotFound("No products were found");
            }

            return products;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
        
    }

    [HttpGet("{id:int}", Name = "GetProduct")]
    public ActionResult<Product> Get(int id)
    {
        try
        {
            var product = _context.Products?.AsNoTracking().FirstOrDefault(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound("Product not found");
            }

            return product;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPost]
    public ActionResult Post(Product product)
    {
        try
        {
            if (product is null)
            {
                return BadRequest();
            }

            _context.Products?.Add(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Get), new { id = product.ProductId });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult Patch(int id, Product product)
    {
        try
        {
            if (id != product.ProductId)
            {
                return NotFound("Product not found");
            }

            _context.Products?.Update(product);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var product = _context.Products?.FirstOrDefault(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound();
            }

            _context.Products?.Remove(product);
            _context.SaveChanges();

            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
}
