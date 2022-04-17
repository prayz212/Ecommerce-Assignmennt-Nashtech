using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Clients;

namespace CustomerSite.Interfaces
{
  public interface IHomeService
  {
    Task<ProductListReadDto> GetFeaturedProductData(int page, int size);
  }

  public interface IProductService
  {
    Task<ProductDetailReadDto> GetProductDetailData(int id);
    Task<ProductListReadDto> GetCategoryProductData(string category, int page, int size);
  }

  public interface ISharedService
  {
    Task<IEnumerable<CategoryReadDto>> GetCategoryData();
  }
}