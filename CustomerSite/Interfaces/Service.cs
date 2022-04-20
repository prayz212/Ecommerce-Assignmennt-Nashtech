using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Clients;

namespace CustomerSite.Interfaces
{
  public interface IHomeService
  {
    
  }

  public interface IProductService
  {
    Task<ProductDetailReadDto> GetProductDetailData(int id);
    Task<ProductListReadDto> GetCategoryProductData(string category, int page, int size);
    Task<ProductListReadDto> GetFeaturedProductData(int page, int size);
    Task<IEnumerable<ProductReadDto>> GetRelativeProducts(int id, int size);
    Task<bool> ProductRating(ProductRatingWriteDto dto);
  }

  public interface ISharedService
  {
    Task<IEnumerable<CategoryReadDto>> GetCategoryData();
  }
}