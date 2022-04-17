using System.Collections.Generic;
using Shared.Clients;

namespace CustomerSite.Models
{
    public class HomeIndexViewModel : BaseViewModel
    {
        public IEnumerable<ProductReadDto> products { get; set; }
    }
}
