using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIs.Interfaces.Client;
using Microsoft.EntityFrameworkCore;
using ViewModels.Clients;

namespace APIs.Repositories.Client
{
  public class CategoryRepository : ICategoryRepository
  {
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IList<CategoryReadDto>> GetCategories()
    {
      return await _context.Categories
      .Select(c => new CategoryReadDto{
        id = c.Id,
        name = c.Name,
        displayName = c.DisplayName,
        description = c.Description
      })
      .ToListAsync();
    }
  }
}