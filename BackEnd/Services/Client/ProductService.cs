using System.Threading.Tasks;
using BackEnd.Interfaces.Client;
using Shared.Clients;

namespace BackEnd.Services.Client
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private const string GET_ALL_PRODUCT = "TatCaSanPham";

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductListReadDto> GetFeatureProduct(int page, int size)
        {
            if (page <= 0 || size <= 0) return null;
            
            var products = await _productRepository.GetFeatureProducts(page, size);
            var count = await _productRepository.CountFeatureProduct();

            var totalPage = this.GetTotalPage(count, size);
            if (totalPage == -1)
            {
                return null;
            }

            return new ProductListReadDto()
            {
                products = products,
                totalPage = totalPage,
                currentPage = page
            };
        }

        public async Task<ProductListReadDto> GetProductByCategory(string category, int page, int size)
        {
            if (string.IsNullOrEmpty(category) || page <= 0 || size <= 0) return null;

            if (category == GET_ALL_PRODUCT) 
            {
                return await this.GetAllProduct(page, size);
            }

            var products = await _productRepository.GetProductByCategory(category, page, size);
            var count = await _productRepository.CountProductByCategory(category);
            
            var totalPage = this.GetTotalPage(count, size);
            if (totalPage == -1)
            {
                return null;
            } 

            return new ProductListReadDto() 
            { 
                products = products,
                totalPage = totalPage,
                currentPage = page,
            };
        }

        public async Task<ProductDetailReadDto> GetProductDetailById(int id)
        {
            if (id <= 0) return null;

            var product = await _productRepository.GetProductDetailById(id);
            return product;
        }

        public async Task<ProductListReadDto> GetAllProduct(int page, int size)
        {
            if (page <= 0 || size <= 0) return null;

            var products = await _productRepository.GetAllProduct(page, size);
            var count = await _productRepository.CountAllProduct();

            var totalPage = this.GetTotalPage(count, size);
            if (totalPage == -1)
            {
                return null;
            }

            return new ProductListReadDto()
            {
                products = products,
                totalPage = totalPage,
                currentPage = page,
            };
        }

        private int GetTotalPage(int count, int size)
        {
            if (count <= 0 || size <= 0) return -1;
            return count % size == 0 ? count / size : (count/ size) + 1;
        }
    }
}