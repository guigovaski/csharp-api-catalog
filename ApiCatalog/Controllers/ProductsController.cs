using Microsoft.AspNetCore.Mvc;
using ApiCatalog.Models;
using ApiCatalog.Repository.Interfaces;
using AutoMapper;
using ApiCatalog.Dtos;
using ApiCatalog.Pagination;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData.Query;

namespace ApiCatalog.Controllers;

[EnableQuery]
[ApiConventionType(typeof(DefaultApiConventions))]
[ApiVersion("1.0")]
[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ProductsController : ControllerBase
{
    private readonly IUnityOfWork _uof;
    private readonly IMapper _mapper;

    public ProductsController(IUnityOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] ProductsPageParameters query)
    {
        try
        {
            var products = await _uof.ProductRepository.GetProductsPaginated(query);

            if (products is null)
            {
                return NotFound("No products were found");
            }

            var metadata = new
            {
                products.TotalCount,
                products.TotalPages,
                products.CurrentPage,
                products.PageSize,
                products.HasNext,
                products.HasPrevious,
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            var productsDto = _mapper.Map<List<ProductDto>>(products);

            return productsDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
        
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        try
        {
            var product = await _uof.ProductRepository.GetById(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound("Product not found");
            }

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post(ProductDto productDto)
    {
        try
        {
            if (productDto is null)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDto);

            _uof.ProductRepository.Add(product);
            await _uof.Commit();

            return RedirectToAction(nameof(Get), new { id = product.ProductId });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Patch(int id, ProductDto productDto)
    {
        try
        {
            if (id != productDto.ProductId)
            {
                return NotFound("Product not found");
            }

            var product = _mapper.Map<Product>(productDto);

            _uof.ProductRepository.Update(product);
            await _uof.Commit();

            return NoContent();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var product = await _uof.ProductRepository.GetById(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound();
            }

            _uof.ProductRepository.Delete(product);
            await _uof.Commit();

            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
}
