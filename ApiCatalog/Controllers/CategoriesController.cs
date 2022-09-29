using ApiCatalog.Dtos;
using ApiCatalog.Models;
using ApiCatalog.Pagination;
using ApiCatalog.Repository.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiCatalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly IMapper _mapper;

    public CategoriesController(IUnityOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> Get([FromQuery] CategoriesPageParameters query)
    {
        try
        {
            var categories = await _uof.CategoryRepository.GetCategoriesPaginated(query);

            if (categories is null)
            {
                return NotFound();
            }

            var metadata = new
            {
                categories.TotalCount,
                categories.TotalPages,
                categories.CurrentPage,
                categories.PageSize,
                categories.HasNext,
                categories.HasPrevious,
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);

            return categoriesDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoryDto>> Get(int id)
    {
        try
        {
            var category = await _uof.CategoryRepository.GetById(c => c.CategoryId == id);

            if (category is null)
            {
                return NotFound("Category not found");
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpGet("products")]
    public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategoriesProducts()
    {
        try
        {
            var categoriesProducts = await _uof.CategoryRepository.GetCategoriesProducts();

            if (categoriesProducts is null)
            {
                return NotFound();
            }

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categoriesProducts);

            return categoriesDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }


    [HttpPost]
    public async Task<ActionResult> Post(CategoryDto categoryDto)
    {
        try
        {
            var category = _mapper.Map<Category>(categoryDto);
            _uof.CategoryRepository.Add(category);
            await _uof.Commit();

            return RedirectToAction(nameof(Get), new { id = category.CategoryId });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id, CategoryDto categoryDto)
    {
        try
        {
            if (id != categoryDto.CategoryId)
            {
                return BadRequest();
            }

            var category = _mapper.Map<Category>(categoryDto);

            _uof.CategoryRepository.Update(category);
            await _uof.Commit();

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var category = await _uof.CategoryRepository.GetById(c => c.CategoryId == id);

            if (category is null)
            {
                return NotFound();
            }

            _uof.CategoryRepository.Delete(category);
            await _uof.Commit();

            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
}
