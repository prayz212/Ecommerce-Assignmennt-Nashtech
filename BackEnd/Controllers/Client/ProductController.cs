using System.Threading.Tasks;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Clients;

namespace BackEnd.Controllers.Client
{
    [ApiController]
    [Route("api/client/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private const int DEFAULT_PAGE_NUMBER = 1;
        private const int DEFAULT_SIZE_PER_PAGE = 6;
        private const int DEFAULT_SIZE_OF_RELATIVE_PRODUCT = 4;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("features")]
        public async Task<IActionResult> GetFeatureProducts([FromQuery] int page = DEFAULT_PAGE_NUMBER, [FromQuery] int size = DEFAULT_SIZE_PER_PAGE)
        {
            var products = await _productService.GetFeatureProduct(page, size);
            if (products is null)
            {
                return BadRequest();
            }
            
            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByCategory([FromQuery] string category, [FromQuery] int page = DEFAULT_PAGE_NUMBER, [FromQuery] int size = DEFAULT_SIZE_PER_PAGE)
        {
            var products = await _productService.GetProductByCategory(category, page, size);
            if (products is null)
            {
                return BadRequest();
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetailById(int id)
        {
            var product = await _productService.GetProductDetailById(id);
            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("relative/{id}")]
        public async Task<IActionResult> GetRelativeProducts(int id, int size = DEFAULT_SIZE_OF_RELATIVE_PRODUCT)
        {
            var products = await _productService.GetRelativeProducts(id, size);
            if (products is null)
            {
                return BadRequest();
            }

            return Ok(products);
        }

        [HttpPost("rating")]
        public async Task<IActionResult> Rating(ProductRatingWriteDto data)
        {
            var result = await _productService.ProductRating(data);
            return result 
                ? Ok()
                : BadRequest();
        }
    }
}