using System;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.Extensions.Logging;

namespace BackEnd.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) {}

        public async Task<Category> GetById(int id)
        {
            try
            {
                return await base.GetBy(c => c.Id == id);
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(CategoryRepository)} GetById function error");
                return null;
            }
        }

        public override bool Delete(Category category)
        {
            try
            {
                category.IsDeleted = true;
                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(CategoryRepository)} Delete function error");
                return false;
            }
        }

        public override async Task<bool> Update(Category category)
        {
            try
            {
                var exist = await GetById(category.Id);
                if (exist is null)
                {
                    category.Id = 0;
                    return await base.Add(category);
                }

                exist.Name = category.Name;
                exist.DisplayName = category.DisplayName;
                exist.Description = category.Description;
                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(CategoryRepository)} Update function error");
                return false;
            }
        }
    }
}