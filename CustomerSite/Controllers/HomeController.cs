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

        public HomeController(IHomeService homeService, ISharedService sharedService)
        {
            _homeService = homeService;
            _sharedService = sharedService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryReadDto> categories = await _sharedService.GetCategoryData();
            IEnumerable<ProductReadDto> products = await _homeService.GetFeaturedProductData();

            var vm = new HomeIndexViewModel()
            {
                categories = categories,
                products = products
            };

            if (products is null)
            {
                return RedirectToAction("Index", "Error");
            }

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