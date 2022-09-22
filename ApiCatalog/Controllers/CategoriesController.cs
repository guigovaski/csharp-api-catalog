using ApiCatalog.Data;
using ApiCatalog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ApiCatalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ApiCatalogContext _context;

    public CategoriesController(ApiCatalogContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> Get()
    {
        try
        {
            var categories = _context.Categories?.AsNoTracking().ToList();

            if (categories is null)
            {
                return NotFound();
            }

            return categories;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("{id:int}")]
    public ActionResult<Category> Get(int id)
    {
        try
        {
            var category = _context.Categories?.AsNoTracking().FirstOrDefault(c => c.CategoryId == id);

            if (category is null)
            {
                return NotFound("Category not found");
            }

            return category;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("products")]
    public ActionResult<IEnumerable<Category>> GetCategoriesProducts()
    {
        try
        {
            var categoriesProducts = _context.Categories?.AsNoTracking().Include(c => c.Products).ToList();

            if (categoriesProducts is null)
            {
                return NotFound();
            }

            return categoriesProducts;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }


    [HttpPost]
    public ActionResult Post(Category category)
    {
        try
        {
            _context.Categories?.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Get), new { id = category.CategoryId });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult Patch(int id, Category category)
    {
        try
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            _context.Categories?.Update(category);
            _context.SaveChanges();

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        try
        {
            var category = _context.Categories?.FirstOrDefault(c => c.CategoryId == id);

            if (category is null)
            {
                return NotFound();
            }

            _context.Categories?.Remove(category);
            _context.SaveChanges();

            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
}
