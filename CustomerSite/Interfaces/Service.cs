using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shared.Clients;

namespace CustomerSite.Interfaces
{
  public interface IHomeService
  {
    Task<IEnumerable<ProductReadDto>> GetFeaturedProductData();
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