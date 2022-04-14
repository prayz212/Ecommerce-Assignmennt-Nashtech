using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIs.Interfaces.Client;
using Microsoft.AspNetCore.Mvc;

namespace APIs.Controllers.Client
{
  [ApiController]
  [Route("api/client/category")]
  public class CategoryController : ControllerBase
  {
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategory()
    {
      var categories = await _categoryService.GetCategories();
      return Ok(categories);
    } 
  }
}