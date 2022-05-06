using System;
using System.Collections.Generic;
using System.Linq;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.Extensions.Logging;

namespace BackEnd.Repositories
{
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext context, ILogger logger) : base(context, logger) {}

        public override bool Delete(Image image)
        {
            try
            {
                image.IsDeleted = true;
                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(ImageRepository)} Delete function error");
                return false;
            }
        }

        public override bool DeleteRange(IEnumerable<Image> images)
        {
            try
            {
                images.ToList().ForEach(image => image.IsDeleted = true);
                return true;
            } 
            catch (Exception e)
            {
                _logger.LogError(e, $"{typeof(ImageRepository)} DeleteRange function error");
                return false;
            }
        }
    }
}