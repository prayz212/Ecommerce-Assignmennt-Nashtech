using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces.Client;
using Shared.Clients;

namespace BackEnd.Services.Client
{
  public class CategoryService : ICategoryService
  {
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
      _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryReadDto>> GetCategories()
    {
      var categories = await _categoryRepository.GetCategories();
      return categories;
    }
  }
}