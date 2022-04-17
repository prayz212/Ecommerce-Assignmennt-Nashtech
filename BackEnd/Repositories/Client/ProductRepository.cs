using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces.Client;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Shared.Clients;

namespace BackEnd.Repositories.Client
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<ProductReadDto>> GetFeatureProducts()
        {
            return await _context.Products
                .Where(p => p.IsFeatured == true)
                .Select(p => new ProductReadDto
                {
                    id = p.Id,
                    name = p.Name,
                    prices = p.Prices,
                    averageRate = p.Ratings.FirstOrDefault() != null ? p.Ratings.Average(r => r.Stars) : 0,
                    thumbnailName = p.Images.FirstOrDefault().Name,
                    thumbnailUri = p.Images.FirstOrDefault().Uri
                })
                .ToListAsync();
        }

        public async Task<IList<ProductReadDto>> GetProductByCategory(string category, int page, int size)
        {
            return await _context.Products
                .Where(p => p.Category.Name == category)
                .Select(p => new ProductReadDto
                {
                    id = p.Id,
                    name = p.Name,
                    prices = p.Prices,
                    averageRate = p.Ratings.FirstOrDefault() != null ? p.Ratings.Average(r => r.Stars) : 0,
                    thumbnailName = p.Images.FirstOrDefault().Name,
                    thumbnailUri = p.Images.FirstOrDefault().Uri
                })
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public async Task<ProductDetailReadDto> GetProductDetailById(int id)
        {
            return await _context.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailReadDto()
                {
                    id = p.Id,
                    name = p.Name,
                    description = p.Description,
                    prices = p.Prices,
                    averageRate = p.Ratings.FirstOrDefault() != null ? p.Ratings.Average(r => r.Stars) : 0,
                    images = p.Images.Select(i => new ImageReadDto() { name = i.Name, uri = i.Uri }).ToList<ImageReadDto>()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int> CountProductByCategory(string category)
        {
            return await _context.Products
                .Where(p => p.Category.Name == category)
                .CountAsync();
        }

        public async Task<IList<ProductReadDto>> GetAllProduct(int page, int size)
        {
            return await _context.Products
                .Select(p => new ProductReadDto()
                {
                    id = p.Id,
                    name = p.Name,
                    prices = p.Prices,
                    averageRate = p.Ratings.FirstOrDefault() != null ? p.Ratings.Average(r => r.Stars) : 0,
                    thumbnailName = p.Images.FirstOrDefault().Name,
                    thumbnailUri = p.Images.FirstOrDefault().Uri
                })
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
        }

        public Task<int> CountAllProduct()
        {
            return _context.Products.CountAsync();
        }
    }
}