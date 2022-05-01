using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerSite.Interfaces;
using CustomerSite.Models;
using Shared.Clients;
using CustomerSite.Utils;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            ProductListReadDto data = await _productService.GetFeaturedProductData(ConstantVariable.DEFAULT_PAGE_NUMBER, ConstantVariable.DEFAULT_SIZE_PER_PAGE);

            if (data is null)
            {
                return RedirectToAction("Index", "Error");
            }

            var vm = new HomeIndexViewModel()
            {
                products = data.products
            };

            return View(vm);
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}