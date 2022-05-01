using System.Threading.Tasks;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models.ViewModels;
using BackEnd.Utils;

namespace BackEnd.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/categories")]
    public class AdminCategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public AdminCategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int page = ConstantVariable.DEFAULT_CATEGORY_PAGE_NUMBER, [FromQuery] int size = ConstantVariable.DEFAULT_CATEGORY_SIZE_PER_PAGE)
        {
            if (page < 1 || size < 1) return BadRequest();
            var categories = await _categoryService.AdminGetCategories(page, size);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryDetail(int id)
        {
            if (id <= 0) return BadRequest();
            var category = await _categoryService.GetCategory(id);
            return category is null ? NotFound() : Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> NewCategory(CreateCategoryDto data)
        {
            var result = await _categoryService.CreateCategory(data);
            return result is null ? BadRequest() : Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDetailDto data)
        {
            var result = await _categoryService.UpdateCategory(data);
            return result is null ? BadRequest() : Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0) return BadRequest();
            var result = await _categoryService.DeleteCategory(id);
            return result ? Ok() : BadRequest();
        }
    }
}