using System.Collections.Generic;
using System.Threading.Tasks;
using APIs.Interfaces.Client;
using ViewModels.Clients;

namespace APIs.Services.Client
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