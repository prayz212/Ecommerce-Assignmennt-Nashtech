using System;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using Microsoft.Extensions.Logging;

namespace BackEnd.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public ICategoryRepository Categories { get; private set; }
        public IProductRepository Products { get; private set; }
        public IRatingRepository Ratings { get; private set; }
        public IImageRepository Images { get; private set; }
        public IAdminRepository Admins { get; private set; }

        public UnitOfWork(ApplicationDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("logs");
            this.Configuration(context, _logger);
        }

        private void Configuration(ApplicationDbContext context, ILogger _logger)
        {
            Categories = new CategoryRepository(context, _logger);
            Products = new ProductRepository(context, _logger);
            Ratings = new RatingRepository(context, _logger);
            Images = new ImageRepository(context, _logger);
            Admins = new AdminRepository(context, _logger);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangeAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(UnitOfWork)} SaveChangeAsync function error");
            }
        }
    }
}