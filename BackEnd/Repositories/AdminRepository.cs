using System;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.Extensions.Logging;

namespace BackEnd.Repositories
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) {}

        public async Task<Admin> GetByAccountId(string id)
        {
            try
            {
                return await base.GetBy(a => a.AccountId == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(AdminRepository)} GetByAccountId function error");
                return null;
            }
        }
    }
}