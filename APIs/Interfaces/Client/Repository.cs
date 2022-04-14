using System.Collections.Generic;
using System.Threading.Tasks;
using APIs.Models;
using ViewModels.Clients;

namespace APIs.Interfaces.Client
{
  public interface IProductRepository
  {
    Task<IList<ProductReadDto>> GetFeatureProducts();
    Task<IList<ProductReadDto>> GetProductByCategory(string category);
  }

  public interface ICategoryRepository
  {
    Task<IList<CategoryReadDto>> GetCategories();
  }
}