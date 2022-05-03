using System.Collections.Generic;

namespace CustomerSite.Models
{
    public class PaginationViewModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
        public string Category { get; set; }
    }
}
