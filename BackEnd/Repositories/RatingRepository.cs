using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Rating>> GetRatingsByProductId(int id)
        {
            return await _context.Ratings
                .Where(r => r.ProductID == id)
                .ToListAsync();
        }

        public async Task<bool> DeleteRatings(IEnumerable<Rating> ratings)
        {
            ratings.ToList().ForEach(rating => rating.IsDeleted = true);
            _context.Ratings.UpdateRange(ratings);
            return await _context.SaveChangesAsync() == ratings.Count();
        }
    }
}