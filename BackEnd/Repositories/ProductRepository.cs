using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.Extensions.Logging;

namespace BackEnd.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) {}

        public async Task<Product> GetById(int id)
        {
            try
            {
                return await base.GetBy(p => p.Id == id, includes: "Category,Images,Ratings");
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(ProductRepository)} GetById function error");
                return null;
            }
        }

        public override async Task<bool> Update(Product product)
        {
            try
            {
                var exist = await GetById(product.Id);
                if (exist is null)
                {
                    product.Id = 0;
                    return await base.Add(product);
                }

                exist.Name = product.Name;
                exist.Description = product.Description;
                exist.Prices = product.Prices;
                exist.IsFeatured = product.IsFeatured;
                exist.UpdatedDate = DateTime.Now;
                exist.CategoryId = product.CategoryId;

                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(ProductRepository)} Update function error");
                return false;
            }
        }

        public override bool Delete(Product product)
        {
            try
            {
                product.IsDeleted = true;
                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(ProductRepository)} Delete function error");
                return false;
            }
        }

        public override bool DeleteRange(IEnumerable<Product> products)
        {
            try
            {
                products.ToList().ForEach(product => product.IsDeleted = true);
                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(ProductRepository)} Delete function error");
                return false;
            }
        }
    }
}