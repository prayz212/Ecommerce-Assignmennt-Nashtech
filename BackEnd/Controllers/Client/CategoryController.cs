using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers.Client
{
  [ApiController]
  [Route("api/client/categories")]
  public class CategoryController : ControllerBase
  {
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
      _categoryService = categoryService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
      var categories = await _categoryService.GetCategories();
      return Ok(categories);
    } 
  }
}