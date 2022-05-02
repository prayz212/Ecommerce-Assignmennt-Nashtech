using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Clients;

namespace BackEnd.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ProductReadDto>> GetFeatureProducts(int page, int size)
        {
            var skip = (page - 1) * size;
            return await _context.Products
                .Where(p => p.IsFeatured == true)
                .Select(p => new ProductReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Prices = p.Prices,
                    AverageRate = p.Ratings.FirstOrDefault() != null ? p.Ratings.Average(r => r.Stars) : 0,
                    ThumbnailName = p.Images.FirstOrDefault().Name,
                    ThumbnailUri = p.Images.FirstOrDefault().Uri
                })
                .Skip(skip)
                .Take(size)
                .ToListAsync();
        }

        public async Task<IList<ProductReadDto>> GetProductsByCategory(string category, int page, int size)
        {
            var skip = (page - 1) * size;
            return await _context.Products
                .Where(p => p.Category.Name == category)
                .Select(p => new ProductReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Prices = p.Prices,
                    AverageRate = p.Ratings.FirstOrDefault() != null ? p.Ratings.Average(r => r.Stars) : 0,
                    ThumbnailName = p.Images.FirstOrDefault().Name,
                    ThumbnailUri = p.Images.FirstOrDefault().Uri
                })
                .Skip(skip)
                .Take(size)
                .ToListAsync();
        }

        public async Task<ProductDetailReadDto> GetProductDetailById(int id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailReadDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Prices = p.Prices,
                    AverageRate = p.Ratings.FirstOrDefault() != null ? p.Ratings.Average(r => r.Stars) : 0,
                    Images = p.Images.Select(i => new ImageReadDto() { Name = i.Name, Uri = i.Uri }).ToList<ImageReadDto>()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IList<ProductReadDto>> GetAllProducts(int page, int size)
        {
            var skip = (page - 1) * size;
            return await _context.Products
                .Select(p => new ProductReadDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Prices = p.Prices,
                    AverageRate = p.Ratings.FirstOrDefault() != null ? p.Ratings.Average(r => r.Stars) : 0,
                    ThumbnailName = p.Images.FirstOrDefault().Name,
                    ThumbnailUri = p.Images.FirstOrDefault().Uri
                })
                .Skip(skip)
                .Take(size)
                .ToListAsync();
        }

        public async Task<IList<ProductReadDto>> GetRelativeProducts(int currentCategoryId, int currentProductId, int size)
        {
            return await _context.Products
                .Where(p => p.CategoryId == currentCategoryId && p.Id != currentProductId)
                .Select(p => new ProductReadDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    Prices = p.Prices,
                    AverageRate = p.Ratings.FirstOrDefault() != null ? p.Ratings.Average(r => r.Stars) : 0,
                    ThumbnailName = p.Images.FirstOrDefault().Name,
                    ThumbnailUri = p.Images.FirstOrDefault().Uri
                })
                .OrderBy(p => Guid.NewGuid())
                .Take(size)
                .ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<int> CountProductsByCategory(string category)
        {
            return await _context.Products
                .Where(p => p.Category.Name == category)
                .CountAsync();
        }

        public Task<int> CountAllProducts()
        {
            return _context.Products.CountAsync();
        }

        public Task<int> CountFeatureProducts()
        {
            return _context.Products
                .Where(p => p.IsFeatured == true)
                .CountAsync();
        }
    }
}