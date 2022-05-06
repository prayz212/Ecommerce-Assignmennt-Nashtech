using System;
using System.Collections.Generic;
using System.Linq;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.Extensions.Logging;

namespace BackEnd.Repositories
{
    public class RatingRepository : GenericRepository<Rating>, IRatingRepository
    {
        public RatingRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) {}

        public override bool Delete(Rating rating)
        {
            try
            {
                rating.IsDeleted = true;
                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(RatingRepository)} Delete function error");
                return false;
            }
        }

        public override bool DeleteRange(IEnumerable<Rating> ratings)
        {
            try
            {
                ratings.ToList().ForEach(rating => rating.IsDeleted = true);
                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(RatingRepository)} DeleteRange function error");
                return false;
            }
        }
    }
}