using System.Collections.Generic;
using Shared.Clients;

namespace CustomerSite.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<ProductReadDto> Products { get; set; }
    }
}
