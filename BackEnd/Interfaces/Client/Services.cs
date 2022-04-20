using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Clients;

namespace BackEnd.Interfaces.Client
{
  public interface IProductService
  {
    Task<ProductListReadDto> GetFeatureProduct(int page, int size);
    Task<ProductListReadDto> GetProductByCategory(string category, int page, int size);
    Task<ProductDetailReadDto> GetProductDetailById(int id);
    Task<ProductListReadDto> GetAllProduct(int page, int size);
    Task<IEnumerable<ProductReadDto>> GetRelativeProducts(int id, int size);
    Task<bool> ProductRating(ProductRatingWriteDto data);
  }

  public interface ICategoryService
  {
    Task<IEnumerable<CategoryReadDto>> GetCategories();
  }
}