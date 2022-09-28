using Microsoft.AspNetCore.Mvc;
using ApiCatalog.Models;
using ApiCatalog.Repository.Interfaces;
using AutoMapper;
using ApiCatalog.Dtos;

namespace ApiCatalog.Controllers;

[Route("api/[controller]")]
[ApiController]
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
    public ActionResult<IEnumerable<ProductDto>> Get()
    {
        try
        {
            var products = _uof.ProductRepository.Get().ToList();

            if (products is null)
            {
                return NotFound("No products were found");
            }

            var productsDto = _mapper.Map<List<ProductDto>>(products);

            return productsDto;
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
        
    }

    [HttpGet("{id:int}")]
    public ActionResult<ProductDto> Get(int id)
    {
        try
        {
            var product = _uof.ProductRepository.GetById(p => p.ProductId == id);

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
    public ActionResult Post(ProductDto productDto)
    {
        try
        {
            if (productDto is null)
            {
                return BadRequest();
            }

            var product = _mapper.Map<Product>(productDto);

            _uof.ProductRepository.Add(product);
            _uof.Commit();

            return RedirectToAction(nameof(Get), new { id = product.ProductId });
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }

    [HttpPut("{id:int}")]
    public ActionResult Patch(int id, ProductDto productDto)
    {
        try
        {
            if (id != productDto.ProductId)
            {
                return NotFound("Product not found");
            }

            var product = _mapper.Map<Product>(productDto);

            _uof.ProductRepository.Update(product);
            _uof.Commit();

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
            var product = _uof.ProductRepository.GetById(p => p.ProductId == id);

            if (product is null)
            {
                return NotFound();
            }

            _uof.ProductRepository.Delete(product);
            _uof.Commit();

            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
}
