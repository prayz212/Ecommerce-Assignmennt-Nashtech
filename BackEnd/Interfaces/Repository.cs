using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Models;
using Shared.Clients;

namespace BackEnd.Interfaces
{
  public interface IProductRepository
  {
    Task<IList<ProductReadDto>> GetFeatureProducts(int page, int size);
    Task<IList<ProductReadDto>> GetProductsByCategory(string category, int page, int size);
    Task<Product> GetProduct(int id);
    Task<IList<Product>> GetProducts(int page, int size);
    Task<IList<ProductReadDto>> GetRelativeProducts(int categoryId, int productId, int size);
    Task<bool> NewProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<int> CountProductsByCategory(string category);
    Task<int> CountAllProducts();
    Task<int> CountFeatureProducts();
  }

  public interface ICategoryRepository
  {
    Task<IList<Category>> GetCategories(int page, int size);
    Task<IList<Category>> GetCategories();
    Task<bool> NewCategory(Category category);
    Task<bool> UpdateCategory(Category category);
    Task<Category> GetCategory(int id);
    Task<int> CountAllCategories();
  }

  public interface IRatingRepository
  {
    Task<bool> CreateProductRating(ProductRatingWriteDto data);
  }

  public interface IImageRepository
  {
    Task<bool> CreateImages(IEnumerable<Image> images);
    Task<IEnumerable<Image>> GetImagesByProductId(int id);
    Task<bool> DeleteImages(IEnumerable<Image> images);
  }
}