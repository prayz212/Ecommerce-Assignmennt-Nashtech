using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Models.ViewModels;
using Shared.Clients;

namespace BackEnd.Interfaces
{
  public interface IProductService
  {
    Task<ProductListReadDto> GetFeatureProduct(int page, int size);
    Task<ProductListReadDto> GetProductByCategory(string category, int page, int size);
    Task<ProductDetailReadDto> GetProductDetailById(int id);
    Task<ProductListReadDto> GetAllProduct(int page, int size);
    Task<IEnumerable<ProductReadDto>> GetRelativeProducts(int id, int size);
    Task<bool> ProductRating(ProductRatingWriteDto data);
    Task<ProductListDto> AdminGetProducts(int page, int size);
    Task<ProductDetailDto> AdminGetProductDetail(int id);
  }

  public interface ICategoryService
  {
    Task<IEnumerable<CategoryReadDto>> GetCategories();
    Task<IEnumerable<CategoryDto>> AdminGetCategories(int page, int size);
    Task<CategoryDetailDto> GetCategory(int id);
    Task<CategoryDetailDto> CreateCategory(CreateCategoryDto dto);
    Task<CategoryDetailDto> UpdateCategory(CategoryDetailDto dto);
    Task<bool> DeleteCategory(int id);
  }
}