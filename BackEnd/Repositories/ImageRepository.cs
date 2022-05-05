using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;

        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateImages(IEnumerable<Image> images)
        {
            await _context.Images.AddRangeAsync(images);
            return await _context.SaveChangesAsync() == images.Count();
        }
        public async Task<bool> DeleteImages(IEnumerable<Image> images)
        {
            images.ToList().ForEach(image => image.IsDeleted = true);
            _context.Images.UpdateRange(images);
            return await _context.SaveChangesAsync() == images.Count();
        }

        public async Task<IEnumerable<Image>> GetImagesByProductId(int id)
        {
            return await _context.Images
                .Where(image => image.ProductId == id)
                .ToListAsync();
        }
    }
}