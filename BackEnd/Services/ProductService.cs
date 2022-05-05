using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BackEnd.Interfaces;
using BackEnd.Models;
using BackEnd.Models.ViewModels;
using BackEnd.Utils;
using Shared.Clients;

namespace BackEnd.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IImageRepository _imageRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IRatingRepository ratingRepository, IImageRepository imageRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _ratingRepository = ratingRepository;
            _imageRepository = imageRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ProductListReadDto> GetFeatureProducts(int page, int size)
        {
            if (page <= 0 || size <= 0) return null;
            
            var count = await _productRepository.CountFeatureProducts();
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
                Products = products,
                TotalPage = totalPage,
                CurrentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<ProductListReadDto> GetProductsByCategory(string category, int page, int size)
        {
            if (string.IsNullOrEmpty(category) || page <= 0 || size <= 0) return null;

            if (string.Equals(category, ConstantVariable.DEFAULT_PRODUCT_CATEGORY)) 
            {
                return await this.GetAllProducts(page, size);
            }

            var count = await _productRepository.CountProductsByCategory(category);
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var products = await _productRepository.GetProductsByCategory(category, page, size);

            return new ProductListReadDto() 
            { 
                Products = _mapper.Map<IEnumerable<ProductReadDto>>(products).ToList(),
                TotalPage = totalPage,
                CurrentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<ProductDetailReadDto> GetProductDetailById(int id)
        {
            if (id <= 0) return null;

            var rawProduct = await _productRepository.GetProduct(id);
            return rawProduct is null
                ? null
                : _mapper.Map<ProductDetailReadDto>(rawProduct);
        }

        public async Task<ProductListReadDto> GetAllProducts(int page, int size)
        {
            if (page <= 0 || size <= 0) return null;

            var count = await _productRepository.CountAllProducts();
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var rawProducts = await _productRepository.GetProducts(page, size);
            var products = _mapper.Map<IEnumerable<ProductReadDto>>(rawProducts);

            return new ProductListReadDto()
            {
                Products = products.ToList(),
                TotalPage = totalPage,
                CurrentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<IEnumerable<ProductReadDto>> GetRelativeProducts(int id, int size)
        {
            if (id <= 0 || size <= 0) return null;

            var product = await _productRepository.GetProduct(id);
            if (product is null) return null;

            var products = await _productRepository.GetRelativeProducts(product.CategoryId, product.Id, size);
            return products;
        }

        public async Task<bool> ProductRating(ProductRatingWriteDto data)
        {
            if (data.ProductID <= 0 || data.Star <= 0)
            {
                return false;
            }

            var product = await _productRepository.GetProduct(data.ProductID);
            if (product is null)
            {
                return false;
            }

            return await _ratingRepository.CreateProductRating(data);
        }

        public async Task<ProductListDto> AdminGetProducts(int page, int size)
        {
            var count = await _productRepository.CountAllProducts();
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var products = await _productRepository.GetProducts(page, size);
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);

            return new ProductListDto
            {
                Products = result,
                TotalPage = totalPage,
                CurrentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<ProductDetailDto> AdminGetProductDetail(int id)
        {
            var product = await _productRepository.GetProduct(id);
            return product is null
                ? null
                : _mapper.Map<ProductDetailDto>(product);
        }

        public async Task<ProductDetailDto> CreateProduct(CreateProductDto dto)
        {
            var category = await _categoryRepository.GetCategory(Int32.Parse(dto.Category));
            if (category is null) return null;

            var newProduct = _mapper.Map<Product>(dto);
            var saveProductResult = await _productRepository.NewProduct(newProduct);

            var newImages = _mapper.Map<IEnumerable<ImageDto>, IEnumerable<Image>>(
                dto.Images, 
                opts => opts.AfterMap((src, des) => {
                    foreach(Image i in des)
                    {
                        i.ProductId = newProduct.Id;
                    }
                })
            );

            var saveImagesResult = await _imageRepository.CreateImages(newImages);

            return saveProductResult && saveImagesResult
                ? await AdminGetProductDetail(newProduct.Id)
                : null;
        }

        public async Task<ProductDetailDto> UpdateProduct(UpdateProductDto dto)
        {
            var product = await _productRepository.GetProduct(dto.Id);
            var category = await _categoryRepository.GetCategory(Int32.Parse(dto.Category));
            if (product is null || category is null) return null;

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.Prices = dto.Prices;
            product.IsFeatured = dto.IsFeatured;
            product.UpdatedDate = DateTime.Now;
            product.CategoryId = Int32.Parse(dto.Category);

            var updateResult = await _productRepository.UpdateProduct(product);

            if (dto.Images.Count() > 0) {
                //Save new images of product
                var newImages = _mapper.Map<IEnumerable<ImageDto>, IEnumerable<Image>>(
                    dto.Images, 
                    opts => opts.AfterMap((src, des) => {
                        foreach(Image i in des)
                        {
                            i.ProductId = product.Id;
                        }
                    })
                );

                updateResult = updateResult && await _imageRepository.CreateImages(newImages);
            }

            //Delete images of product
            if (dto.DeletedImages.Count() > 0) {
                var deleteList = dto.DeletedImages.Select(delete => delete.Uri);
                var images = await _imageRepository.GetImagesByProductId(product.Id);
                var deletedImages = images.Where(image => deleteList.Contains(image.Uri)).ToList();
                updateResult = updateResult && await _imageRepository.DeleteImages(deletedImages);
            }

            return updateResult
                ? await AdminGetProductDetail(product.Id)
                : null;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProduct(id);
            if (product is null) return false;

            var deleteResult = await _productRepository.DeleteProduct(product);

            var images = await _imageRepository.GetImagesByProductId(id);
            deleteResult = deleteResult && await _imageRepository.DeleteImages(images);

            var ratings = await _ratingRepository.GetRatingsByProductId(id);
            deleteResult = deleteResult && await _ratingRepository.DeleteRatings(ratings);

            return deleteResult;
        }

        // public async Task<bool> DeleteProductsByCategory(Category category)
        // {
        //     var products = await _productRepository.GetProductsByCategory(category.Name);
        //     var deleteResult = await _productRepository.DeleteProducts(products);
        // }
    }
}