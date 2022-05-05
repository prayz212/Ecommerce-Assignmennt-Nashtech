using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Models;
using BackEnd.Models.ViewModels;
using Shared.Clients;

namespace BackEnd.Interfaces
{
  public interface IProductService
  {
    Task<ProductListReadDto> GetFeatureProducts(int page, int size);
    Task<ProductListReadDto> GetProductsByCategory(string category, int page, int size);
    Task<ProductDetailReadDto> GetProductDetailById(int id);
    Task<ProductListReadDto> GetAllProducts(int page, int size);
    Task<IEnumerable<ProductReadDto>> GetRelativeProducts(int id, int size);
    Task<bool> ProductRating(ProductRatingWriteDto data);
    Task<ProductListDto> AdminGetProducts(int page, int size);
    Task<ProductDetailDto> AdminGetProductDetail(int id);
    Task<ProductDetailDto> CreateProduct(CreateProductDto dto);
    Task<ProductDetailDto> UpdateProduct(UpdateProductDto dto);
    Task<bool> DeleteProduct(int id);
    // Task<bool> DeleteProductsByCategory(Category category);
  }

  public interface ICategoryService
  {
    Task<IEnumerable<CategoryReadDto>> GetCategories();
    Task<CategoryListDto> AdminGetCategories(int page, int size);
    Task<CategoryDetailDto> GetCategory(int id);
    Task<CategoryDetailDto> CreateCategory(CreateCategoryDto dto);
    Task<CategoryDetailDto> UpdateCategory(CategoryDetailDto dto);
    Task<bool> DeleteCategory(int id);
  }
}