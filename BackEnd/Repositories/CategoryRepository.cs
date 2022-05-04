using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<Category>> GetCategories()
        {
            return await _context.Categories
                .ToListAsync();
        }

        public async Task<IList<Category>> GetCategories(int page, int size)
        {
            var skip = (page - 1) * size;
            return await _context.Categories
                .Skip(skip)
                .Take(size)
                .ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> NewCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            return await _context.SaveChangesAsync() >= 1;
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            return await _context.SaveChangesAsync() >= 1;
        }

        public async Task<int> CountAllCategories()
        {
            return await _context.Categories.CountAsync();
        }
    }
}