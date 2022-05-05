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

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.Ratings)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IList<Product>> GetProducts(int page, int size)
        {
            var skip = (page - 1) * size;
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Images)
                .Include(p => p.Ratings)
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

        public async Task<bool> NewProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() == 1;
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