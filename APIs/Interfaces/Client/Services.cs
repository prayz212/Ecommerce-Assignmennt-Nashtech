using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using APIs.Models;
using ViewModels.Clients;

namespace APIs.Interfaces.Client
{
  public interface IProductService
  {
    Task<IEnumerable<ProductReadDto>> GetFeatureProduct();
    Task<IEnumerable<ProductReadDto>> GetProductByCategory(string category);
  }

  public interface ICategoryService
  {
    Task<IEnumerable<CategoryReadDto>> GetCategories();
  }
}