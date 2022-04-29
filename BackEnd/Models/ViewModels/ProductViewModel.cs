using System.Collections.Generic;
using Shared.Clients;

namespace BackEnd.Models.ViewModels
{
    public class ProductDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int prices { get; set; }
        public int isFeatured { get; set; }
        public string category { get; set; }
    }

    public class ProductDetailDto : ProductDto
    {
        public IList<ImageReadDto> images { get; set; }
        public string description { get; set; }
        public double averageRate { get; set; }
        public string createdAt { get; set; }
        public string updatedDate { get; set; }
    }
}