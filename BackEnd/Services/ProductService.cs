using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.Interfaces;
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

        public async Task<ProductListReadDto> GetFeatureProducts(int page, int size)
        {
            if (page <= 0 || size <= 0) return null;
            
            var products = await _productRepository.GetFeatureProducts(page, size);
            var count = await _productRepository.CountFeatureProducts();

            var totalPage = this.GetTotalPage(count, size);
            if (totalPage == -1 || (totalPage != 0 && totalPage < page))
            {
                return null;
            }

            return new ProductListReadDto()
            {
                products = products,
                totalPage = totalPage,
                currentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<ProductListReadDto> GetProductsByCategory(string category, int page, int size)
        {
            if (string.IsNullOrEmpty(category) || page <= 0 || size <= 0) return null;

            if (string.Equals(category, GET_ALL_PRODUCT)) 
            {
                return await this.GetAllProducts(page, size);
            }

            var products = await _productRepository.GetProductsByCategory(category, page, size);
            var count = await _productRepository.CountProductsByCategory(category);
            
            var totalPage = this.GetTotalPage(count, size);
            if (totalPage == -1 || (totalPage != 0 && totalPage < page))
            {
                return null;
            } 

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

            var product = await _productRepository.GetProductDetailById(id);
            return product;
        }

        public async Task<ProductListReadDto> GetAllProducts(int page, int size)
        {
            if (page <= 0 || size <= 0) return null;

            var products = await _productRepository.GetAllProducts(page, size);
            var count = await _productRepository.CountAllProducts();

            var totalPage = this.GetTotalPage(count, size);
            if (totalPage == -1 || (totalPage != 0 && totalPage < page))
            {
                return null;
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

            var product = await _productRepository.GetProductById(id);
            if (product is null) return null;

            var products = await _productRepository.GetRelativeProducts(product.CategoryId, product.Id, size);
            return products;
        }

        public async Task<bool> ProductRating(ProductRatingWriteDto data)
        {
            if (data.productID <= 0 || data.star <= 0)
            {
                return false;
            }

            var product = await _productRepository.GetProductDetailById(data.productID);
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
    }
}