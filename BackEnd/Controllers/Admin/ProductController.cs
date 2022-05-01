using System.Threading.Tasks;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/products")]
    public class AdminProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private const int DEFAULT_PAGE_NUMBER = 1;
        private const int DEFAULT_SIZE_PER_PAGE = 10;

        public AdminProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int page = DEFAULT_PAGE_NUMBER, [FromQuery] int size = DEFAULT_SIZE_PER_PAGE)
        {
            if (page < 1 || size < 1) return BadRequest();
            var products = await _productService.AdminGetProducts(page, size);
            return products is null ? BadRequest() : Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductDetail(int id)
        {
            if (id <= 0) return BadRequest();
            var product = await _productService.AdminGetProductDetail(id);
            return product is null ? NotFound() : Ok(product);
        }
    }
}