using System.Collections.Generic;
using Shared.Clients;

namespace CustomerSite.Models
{
    public class HomeIndexViewModel
    {
        public IEnumerable<ProductReadDto> products { get; set; }
    }
}
