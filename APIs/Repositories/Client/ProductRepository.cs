using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIs.Interfaces.Client;
using APIs.Models;
using Microsoft.EntityFrameworkCore;
using ViewModels.Clients;

namespace APIs.Repositories.Client
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

    public async Task<IList<ProductReadDto>> GetProductByCategory(string category)
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
        .ToListAsync();
    }
  }
}