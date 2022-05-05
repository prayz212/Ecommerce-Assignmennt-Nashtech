using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models.ViewModels;
using BackEnd.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/products")]
    public class AdminProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public AdminProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] int page = ConstantVariable.DEFAULT_PRODUCT_PAGE_NUMBER, [FromQuery] int size = ConstantVariable.DEFAULT_ADMIN_PRODUCT_SIZE_PER_PAGE)
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

        [HttpPost]
        public async Task<IActionResult> NewProduct(CreateProductDto data)
        {
            var result = await _productService.CreateProduct(data);
            return result is null
                ? BadRequest()
                : CreatedAtAction(
                    nameof(GetProductDetail),
                    new { id = result.Id },
                    result
                );
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto data)
        {
            var result = await _productService.UpdateProduct(data);
            return result is null
                ? BadRequest()
                : Ok(result);
        }
    }
}