using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Models;
using Shared.Clients;

namespace BackEnd.Interfaces.Client
{
  public interface IProductRepository
  {
    Task<IList<ProductReadDto>> GetFeatureProducts();
    Task<IList<ProductReadDto>> GetProductByCategory(string category, int page, int size);
    Task<ProductDetailReadDto> GetProductDetailById(int id);
    Task<int> CountProductByCategory(string category);
    Task<IList<ProductReadDto>> GetAllProduct(int page, int size);
    Task<int> CountAllProduct();
  }

  public interface ICategoryRepository
  {
    Task<IList<CategoryReadDto>> GetCategories();
  }
}