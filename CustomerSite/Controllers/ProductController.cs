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
        private const int DEFAULT_PAGE_NUMBER = 1;
        private const int DEFAULT_SIZE_PER_PAGE = 9;
        private const int NUMBER_OF_RELATIVE_PRODUCT = 4;

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
                    controller = "Product",
                    action = "Index"
                }
            };

            ViewData["CurrentCategory"] = category;
            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            IEnumerable<CategoryReadDto> categories = await _sharedService.GetCategoryData();
            ProductDetailReadDto productDetail = await _productService.GetProductDetailData(id);
            IEnumerable<ProductReadDto> relative = await _productService.GetRelativeProducts(id, NUMBER_OF_RELATIVE_PRODUCT);

            if (productDetail is null)
            {
                return RedirectToAction("Index", "Error");
            }

            var vm = new ProductDetailViewModel()
            {
                categories = categories,
                product = productDetail,
                relative = relative
            };

            return View(vm);
        }

        public async Task<IActionResult> Featured(int page = DEFAULT_PAGE_NUMBER)
        {
            IEnumerable<CategoryReadDto> categories = await _sharedService.GetCategoryData();
            ProductListReadDto data = await _productService.GetFeaturedProductData(page, DEFAULT_SIZE_PER_PAGE);

            if (data is null)
            {
                return RedirectToAction("Index", "Error");
            }

            var vm = new ProductListViewModel()
            {
                categories = categories,
                products = data.products,
                pagination = new PaginationViewModel()
                {
                    currentPage = data.currentPage,
                    totalPage = data.totalPage,
                    controller = "Product",
                    action = "Featured",
                    category = null
                }
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Rating(ProductRatingWriteDto data)
        {
            var result = await _productService.ProductRating(data);
            return result ? Ok() : BadRequest();
        }
    }
}
