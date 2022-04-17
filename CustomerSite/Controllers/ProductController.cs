using Microsoft.AspNetCore.Mvc;
using CustomerSite.Interfaces;
using CustomerSite.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Clients;

namespace CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly ISharedService _sharedService;
        private readonly IProductService _productService;
        private const int DEFAULT_SIZE_PER_PAGE = 9;

        public ProductController(ISharedService sharedService, IProductService productService)
        {
            _sharedService = sharedService;
            _productService = productService;
        }

        public async Task<IActionResult> Index(string category, int page = 1)
        {
            if (category is null) { return RedirectToAction("Index", "Home"); }

            IEnumerable<CategoryReadDto> categories = await _sharedService.GetCategoryData();
            ProductListReadDto data = await _productService.GetCategoryProductData(category, page, DEFAULT_SIZE_PER_PAGE);

            if (data is null)
            {
                return RedirectToAction("Index", "Error");
            }

            var vm = new ProductListViewModel
            {
                categories = categories,
                products = data.products,
                pagination = new PaginationViewModel
                {
                    totalPage = data.totalPage,
                    currentPage = data.currentPage,
                    category = category,
                }
            };

            ViewData["CurrentCategory"] = category;
            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            IEnumerable<CategoryReadDto> categories = await _sharedService.GetCategoryData();
            ProductDetailReadDto productDetail = await _productService.GetProductDetailData(id);

            if (productDetail is null)
            {
                return RedirectToAction("Index", "Error");
            }

            var vm = new ProductDetailViewModel()
            {
                categories = categories,
                product = productDetail,
            };

            return View(vm);
        }
    }
}
