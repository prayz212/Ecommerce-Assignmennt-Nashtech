using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerSite.Interfaces;
using CustomerSite.Models;
using Shared.Clients;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;
        private readonly ISharedService _sharedService;
        private readonly IProductService _productService;
        private const int SHOW_ITEM_NUMBER = 12;
        private const int SHOW_ITEM_PAGE = 1;

        public HomeController(IHomeService homeService, ISharedService sharedService, IProductService productService)
        {
            _homeService = homeService;
            _sharedService = sharedService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryReadDto> categories = await _sharedService.GetCategoryData();
            ProductListReadDto data = await _productService.GetFeaturedProductData(SHOW_ITEM_PAGE, SHOW_ITEM_NUMBER);

            if (data is null)
            {
                return RedirectToAction("Index", "Error");
            }

            var vm = new HomeIndexViewModel()
            {
                categories = categories,
                products = data.products
            };

            return View(vm);
        }

        public async Task<IActionResult> About()
        {
            IEnumerable<CategoryReadDto> categories = await _sharedService.GetCategoryData();
            var vm = new BaseViewModel()
            {
                categories = categories
            };

            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}