using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerSite.Utils;
using Shared.Clients;

namespace CustomerSite.Interfaces
{
  public interface IProductService
  {
    Task<ProductDetailReadDto> GetProductDetailData(int id);
    Task<ProductListReadDto> GetCategoryProductData(string category, int page, int size);
    Task<ProductListReadDto> GetFeaturedProductData(int page, int size);
    Task<IEnumerable<ProductReadDto>> GetRelativeProductData(int id, int size);
    Task<int> ProductRating(ProductRatingWriteDto dto);
  }

  public interface ISharedService
  {
    Task<IEnumerable<CategoryReadDto>> GetCategoryData();
  }

  public interface IAccountService
  {
    Task<TokenDto> Login(LoginDto dto);
    Task<bool> Register(ClientRegisterDto dto);
  }
}