using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Clients;

namespace MVC.Interfaces
{
  public interface IHomeService
  {
    Task<IEnumerable<ProductReadDto>> GetFeaturedProductData();
  }

  public interface ISharedService
  {
    Task<IEnumerable<CategoryReadDto>> GetCategoryData();
  }
}