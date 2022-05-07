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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ProductListReadDto> GetFeatureProducts(int page, int size)
        {
            if (page <= 0 || size <= 0) return null;
            
            var count = await _unitOfWork.Products.CountAll(p => p.IsFeatured == true);
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var rawProducts = await _unitOfWork.Products.GetAll(
                filter: p => p.IsFeatured == true, 
                page: page, 
                size: size, 
                includes: "Images,Ratings"
            );
            var products = _mapper.Map<IEnumerable<ProductReadDto>>(rawProducts);

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

            var count = await _unitOfWork.Products.CountAll(p => p.Category.Name == category);
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var rawProducts = await _unitOfWork.Products.GetAll(
                filter: p => p.Category.Name == category, 
                page: page, 
                size: size, 
                includes: "Images,Ratings,Category"
            );
            var products = _mapper.Map<IEnumerable<ProductReadDto>>(rawProducts);

            return new ProductListReadDto() 
            { 
                Products = products,
                TotalPage = totalPage,
                CurrentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<ProductDetailReadDto> GetProductDetailById(int id)
        {
            if (id <= 0) return null;

            var rawProduct = await _unitOfWork.Products.GetById(id);
            return rawProduct is null
                ? null
                : _mapper.Map<ProductDetailReadDto>(rawProduct);
        }

        public async Task<ProductListReadDto> GetAllProducts(int page, int size)
        {
            if (page <= 0 || size <= 0) return null;

            var count = await _unitOfWork.Products.CountAll();
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var rawProducts = await _unitOfWork.Products.GetAll(page: page, size: size, includes: "Images,Ratings,Category");
            var products = _mapper.Map<IEnumerable<ProductReadDto>>(rawProducts);

            return new ProductListReadDto()
            {
                Products = products,
                TotalPage = totalPage,
                CurrentPage = totalPage > 0 ? page : 0,
            };
        }

        public async Task<IEnumerable<ProductReadDto>> GetRelativeProducts(int id, int size)
        {
            if (id <= 0 || size <= 0) return null;

            var product = await _unitOfWork.Products.GetById(id);
            if (product is null) return null;

            var rawProducts = await _unitOfWork.Products.GetAll(
                filter: p => p.CategoryId == product.CategoryId && p.Id != product.Id,
                orderBy: p => p.OrderBy(p => Guid.NewGuid()), 
                includes: "Images,Ratings"
            );
            rawProducts = rawProducts.Take(size);

            var products = _mapper.Map<IEnumerable<ProductReadDto>>(rawProducts);
            return products;
        }

        public async Task<bool> ProductRating(ProductRatingWriteDto data)
        {
            if (data.ProductID <= 0 || data.Stars <= 0)
            {
                return false;
            }

            var product = await _unitOfWork.Products.GetById(data.ProductID);
            if (product is null)
            {
                return false;
            }

            var rating = _mapper.Map<Rating>(data);
            var saveResult = await _unitOfWork.Ratings.Add(rating);
            await _unitOfWork.SaveChangeAsync();

            return saveResult;
        }

        public async Task<ProductListDto> AdminGetProducts(int page, int size)
        {
            var count = await _unitOfWork.Products.CountAll();
            var totalPage = this.GetTotalPage(count, size);

            //first condition: total page = -1 mean GetTotalPage function occurred error
            //second condition: count > 0 but total page less than passing page
            if (totalPage == -1 || (totalPage > 0 && totalPage < page))
            {
                return null;
            }

            var products = await _unitOfWork.Products.GetAll(page: page, size: size, includes: "Category");
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
            var product = await _unitOfWork.Products.GetById(id);
            return product is null
                ? null
                : _mapper.Map<ProductDetailDto>(product);
        }

        public async Task<ProductDetailDto> CreateProduct(CreateProductDto dto)
        {
            var category = await _unitOfWork.Categories.GetById(dto.Category);
            if (category is null) return null;

            var newProduct = _mapper.Map<Product>(dto);
            var saveProductResult = await _unitOfWork.Products.Add(newProduct);

            await _unitOfWork.SaveChangeAsync();
            if (!saveProductResult) return null;

            var newImages = _mapper.Map<IEnumerable<ImageDto>, IEnumerable<Image>>(
                dto.Images, 
                opts => opts.AfterMap((src, des) => {
                    foreach(Image i in des)
                    {
                        i.ProductId = newProduct.Id;
                    }
                })
            );

            var saveImagesResult = await _unitOfWork.Images.AddRange(newImages);
            await _unitOfWork.SaveChangeAsync();

            return saveProductResult && saveImagesResult
                ? await AdminGetProductDetail(newProduct.Id)
                : null;
        }

        public async Task<ProductDetailDto> UpdateProduct(UpdateProductDto dto)
        {
            var category = await _unitOfWork.Categories.GetById(dto.Category);
            if (category is null) return null;

            var updateProduct = _mapper.Map<Product>(dto);
            var updateResult = await _unitOfWork.Products.Update(updateProduct);

            //Save new images of product
            if (dto.Images.Count() > 0) {
                var newImages = _mapper.Map<IEnumerable<ImageDto>, IEnumerable<Image>>(
                    dto.Images, 
                    opts => opts.AfterMap((src, des) => {
                        foreach(Image i in des)
                        {
                            i.ProductId = updateProduct.Id;
                        }
                    })
                );

                updateResult = updateResult && await _unitOfWork.Images.AddRange(newImages);
            }

            //Delete images of product
            if (dto.DeletedImages.Count() > 0) {
                var deleteList = dto.DeletedImages.Select(delete => delete.Uri);
                var images = await _unitOfWork.Images.GetAll(filter: i => i.ProductId == updateProduct.Id);
                var deletedImages = images.Where(image => deleteList.Contains(image.Uri)).ToList();

                if (deletedImages.Count != deleteList.Count()) return null;
                updateResult = updateResult && _unitOfWork.Images.DeleteRange(deletedImages);
            }

            await _unitOfWork.SaveChangeAsync();

            return updateResult
                ? await AdminGetProductDetail(updateProduct.Id)
                : null;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _unitOfWork.Products.GetById(id);
            if (product is null) return false;

            var images = await _unitOfWork.Images.GetAll(filter: i => i.ProductId == id);
            var ratings = await _unitOfWork.Ratings.GetAll(filter: r => r.ProductID == id);

            var deleteResult = _unitOfWork.Products.Delete(product)
                && _unitOfWork.Images.DeleteRange(images)
                && _unitOfWork.Ratings.DeleteRange(ratings);

            await _unitOfWork.SaveChangeAsync();
            return deleteResult;
        }
    }
}