using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models;
using Shared.Clients;

namespace BackEnd.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _context;

        public RatingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductRating(ProductRatingWriteDto data)
        {
            await _context.Ratings.AddAsync(new Rating() {
                ProductID = data.ProductID,
                Stars = data.Star,
            });

            return await _context.SaveChangesAsync() == 1;
        }
    }
}