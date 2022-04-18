using System.Collections.Generic;

namespace CustomerSite.Models
{
    public class PaginationViewModel
    {
        public string controller { get; set; }
        public string action { get; set; }
        public int totalPage { get; set; }
        public int currentPage { get; set; }
        public string category { get; set; }
    }
}
