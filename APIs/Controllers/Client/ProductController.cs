using System.Threading.Tasks;
using APIs.Interfaces.Client;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers.Client
{
  [ApiController]
  [Route("api/client/products")]
  public class ProductController : ControllerBase
  {
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
      _productService = productService;
    }

    [HttpGet("features")]
    public async Task<IActionResult> GetFeatureProducts()
    {
      var products = await _productService.GetFeatureProduct();
      return Ok(products);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductByCategory([FromQuery] string category)
    {
      if (category is null)
      {
        return BadRequest();
      }

      var products = await _productService.GetProductByCategory(category);
      return Ok(products);
    }
  }
}