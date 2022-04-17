using System.Collections.Generic;
using Shared.Clients;

namespace CustomerSite.Models
{
    public class ProductDetailViewModel : BaseViewModel
    {
        public ProductDetailReadDto product { get; set; }
    }

    public class ProductListViewModel : BaseViewModel
    {
        public IEnumerable<ProductReadDto> products { get; set; }
        public PaginationViewModel pagination { get; set; }
    }
}
