using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerSite.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Shared.Clients;

namespace CustomerSite.Pages.Product
{
    public class Featured : PageModel
    {
        private readonly IProductService _productService;
        private readonly ISharedService _sharedService;
        private const int DEFAULT_PAGE_NUMBER = 1;
        private const int DEFAULT_SIZE_PER_PAGE = 9;
        public ProductListReadDto ProductList { get; set; }
        public List<CategoryReadDto> Category { get; set; }
        
        [FromQuery(Name = "page")]
        public int PageNumber { get; set; } = DEFAULT_PAGE_NUMBER;

        public Featured(IProductService productService, ISharedService sharedService)
        {
            _productService = productService;
            _sharedService = sharedService;
        }

        public async Task<IActionResult> OnGet()
        {
            ProductList = await _productService.GetFeaturedProductData(PageNumber, DEFAULT_SIZE_PER_PAGE);
            Category = new List<CategoryReadDto>(await _sharedService.GetCategoryData());

            return Page();
        }
    }
}