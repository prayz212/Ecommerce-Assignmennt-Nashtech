using System.Collections.Generic;
using Shared.Clients;

namespace BackEnd.Models.ViewModels
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Prices { get; set; }
        public bool IsFeatured { get; set; }
        public string Category { get; set; }
    }

    public class ProductDetailDto : ProductDto
    {
        public IList<ImageReadDto> Images { get; set; }
        public string Description { get; set; }
        public double AverageRate { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }

    public class ProductListDto
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}