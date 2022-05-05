using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Models;
using Shared.Clients;

namespace BackEnd.Interfaces
{
  public interface IProductRepository
  {
    Task<IList<ProductReadDto>> GetFeatureProducts(int page, int size);
    Task<IEnumerable<Product>> GetProductsByCategory(string category, int page, int size);
    Task<IEnumerable<Product>> GetProductsByCategory(string category);
    Task<Product> GetProduct(int id);
    Task<IList<Product>> GetProducts(int page, int size);
    Task<IList<ProductReadDto>> GetRelativeProducts(int categoryId, int productId, int size);
    Task<bool> NewProduct(Product product);
    Task<bool> UpdateProduct(Product product);
    Task<bool> UpdateProducts(IEnumerable<Product> products);
    Task<bool> DeleteProduct(Product product);
    Task<bool> DeleteProducts(IEnumerable<Product> products);
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
    Task<bool> DeleteCategory(Category category);
    Task<Category> GetCategory(int id);
    Task<int> CountAllCategories();
  }

  public interface IRatingRepository
  {
    Task<bool> CreateProductRating(ProductRatingWriteDto data);
    Task<IEnumerable<Rating>> GetRatingsByProductId(int id);
    Task<bool> DeleteRatings(IEnumerable<Rating> ratings);
  }

  public interface IImageRepository
  {
    Task<bool> CreateImages(IEnumerable<Image> images);
    Task<IEnumerable<Image>> GetImagesByProductId(int id);
    Task<bool> DeleteImages(IEnumerable<Image> images);
  }
}