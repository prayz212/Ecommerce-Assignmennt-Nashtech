using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Models.ViewModels;
using Shared.Clients;

namespace BackEnd.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRatingRepository _ratingRepository;
        private const string GET_ALL_PRODUCT = "TatCaSanPham";

        public ProductService(IProductRepository productRepository, IRatingRepository ratingRepository)
        {
            _productRepository = productRepository;
            _ratingRepository = ratingRepository;
        }

        public async Task<ProductListReadDto> GetFeatureProduct(int page, int size)
        {
            if (page <= 0 || size <= 0) return null;
            
            var count = await _productRepository.CountFeatureProduct();
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var products = await _productRepository.GetFeatureProducts(page, size);

            return new ProductListReadDto()
            {
                products = products,
                totalPage = totalPage,
                currentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<ProductListReadDto> GetProductByCategory(string category, int page, int size)
        {
            if (string.IsNullOrEmpty(category) || page <= 0 || size <= 0) return null;

            if (string.Equals(category, GET_ALL_PRODUCT)) 
            {
                return await this.GetAllProduct(page, size);
            }

            var count = await _productRepository.CountProductByCategory(category);
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var products = await _productRepository.GetProductByCategory(category, page, size);

            return new ProductListReadDto() 
            { 
                products = products,
                totalPage = totalPage,
                currentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<ProductDetailReadDto> GetProductDetailById(int id)
        {
            if (id <= 0) return null;

            var rawProduct = await _productRepository.GetProduct(id);
            return rawProduct is null
                ? null
                : new ProductDetailReadDto
                {
                    id = rawProduct.Id,
                    name = rawProduct.Name,
                    description = rawProduct.Description,
                    prices = rawProduct.Prices,
                    averageRate = rawProduct.Ratings.FirstOrDefault() is not null ? rawProduct.Ratings.Average(r => r.Stars) : 0,
                    images = rawProduct.Images.Select(i => new ImageReadDto { name = i.Name, uri = i.Uri }).ToList<ImageReadDto>()
                };
        }

        public async Task<ProductListReadDto> GetAllProduct(int page, int size)
        {
            if (page <= 0 || size <= 0) return null;

            var count = await _productRepository.CountAllProduct();
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var rawProducts = await _productRepository.GetProducts(page, size);
            var products = new List<ProductReadDto>();
            if (rawProducts is not null && rawProducts.Count > 0)
            {
                foreach(Product product in rawProducts)
                {
                    var element = new ProductReadDto
                    {
                        id = product.Id,
                        name = product.Name,
                        prices = product.Prices,
                        averageRate = product.Ratings.FirstOrDefault() is not null ? product.Ratings.Average(r => r.Stars) : 0,
                        thumbnailName = product.Images.FirstOrDefault().Name,
                        thumbnailUri = product.Images.FirstOrDefault().Uri
                    };

                    products.Add(element);
                }
            }

            return new ProductListReadDto()
            {
                products = products,
                totalPage = totalPage,
                currentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<IEnumerable<ProductReadDto>> GetRelativeProducts(int id, int size)
        {
            if (id <= 0 || size <= 0) return null;

            var product = await _productRepository.GetProduct(id);
            if (product is null) return null;

            var products = await _productRepository.GetRelativeProduct(product.CategoryId, product.Id, size);
            return products;
        }

        public async Task<bool> ProductRating(ProductRatingWriteDto data)
        {
            if (data.productID <= 0 || data.star <= 0)
            {
                return false;
            }

            var product = await _productRepository.GetProduct(data.productID);
            if (product is null)
            {
                return false;
            }

            return await _ratingRepository.CreateProductRating(data);
        }

        private int GetTotalPage(int count, int size)
        {
            if (count < 0 || size <= 0) return -1;
            return count % size == 0 ? count / size : (count/ size) + 1;
        }

        public Task<IEnumerable<ProductDto>> AdminGetProducts(int page, int size)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductDetailDto> AdminGetProductDetail(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}