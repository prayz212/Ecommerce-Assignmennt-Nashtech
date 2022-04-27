using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Models;
using Shared.Clients;

namespace BackEnd.Interfaces
{
  public interface IProductRepository
  {
    Task<IList<ProductReadDto>> GetFeatureProducts(int page, int size);
    Task<IList<ProductReadDto>> GetProductByCategory(string category, int page, int size);
    Task<ProductDetailReadDto> GetProductDetailById(int id);
    Task<IList<ProductReadDto>> GetAllProduct(int page, int size);
    Task<IList<ProductReadDto>> GetRelativeProduct(int categoryId, int productId, int size);
    Task<Product> GetProductById(int id);
    Task<int> CountProductByCategory(string category);
    Task<int> CountAllProduct();
    Task<int> CountFeatureProduct();
  }

  public interface ICategoryRepository
  {
    Task<IList<Category>> GetCategories(int page, int size);
    Task<IList<Category>> GetCategories();
    Task<bool> NewCategory(Category category);
    Task<bool> UpdateCategory(Category category);
    Task<Category> GetCategory(int id);
  }

  public interface IRatingRepository
  {
    Task<bool> CreateProductRating(ProductRatingWriteDto data);
  }
}