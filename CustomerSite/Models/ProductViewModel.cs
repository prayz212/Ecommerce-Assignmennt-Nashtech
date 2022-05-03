using System.Collections.Generic;
using Shared.Clients;

namespace CustomerSite.Models
{
    public class ProductDetailViewModel
    {
        public ProductDetailReadDto Product { get; set; }
    }

    public class ProductListViewModel
    {
        public IEnumerable<ProductReadDto> Products { get; set; }
        public PaginationViewModel Pagination { get; set; }
    }
}
