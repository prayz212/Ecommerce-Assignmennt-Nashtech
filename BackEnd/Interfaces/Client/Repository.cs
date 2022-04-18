using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Clients;

namespace BackEnd.Interfaces.Client
{
  public interface IProductRepository
  {
    Task<IList<ProductReadDto>> GetFeatureProducts(int page, int size);
    Task<IList<ProductReadDto>> GetProductByCategory(string category, int page, int size);
    Task<ProductDetailReadDto> GetProductDetailById(int id);
    Task<IList<ProductReadDto>> GetAllProduct(int page, int size);
    Task<int> CountProductByCategory(string category);
    Task<int> CountAllProduct();
    Task<int> CountFeatureProduct();
  }

  public interface ICategoryRepository
  {
    Task<IList<CategoryReadDto>> GetCategories();
  }
}