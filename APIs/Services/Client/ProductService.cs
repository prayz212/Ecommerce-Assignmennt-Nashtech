using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIs.Interfaces.Client;
using ViewModels.Clients;

namespace APIs.Services.Client
{
  public class ProductService : IProductService
  {
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
      _productRepository = productRepository;
    }

    public async Task<IEnumerable<ProductReadDto>> GetFeatureProduct()
    {
      var products = await _productRepository.GetFeatureProducts();
      return products;
    }

    public async Task<IEnumerable<ProductReadDto>> GetProductByCategory(string category)
    {
      var products = await _productRepository.GetProductByCategory(category);
      return products;
    }
  }
}