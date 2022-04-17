

using System.Collections.Generic;
using Shared.Clients;

namespace CustomerSite.Models
{
    public class BaseViewModel
    {
        public IEnumerable<CategoryReadDto> categories { get; set; }
    }
}
