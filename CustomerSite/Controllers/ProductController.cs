using Microsoft.AspNetCore.Mvc;
using CustomerSite.Interfaces;
using CustomerSite.Models;
using System.Threading.Tasks;
using Shared.Clients;
using CustomerSite.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CustomerSite.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index(string category, int page = ConstantVariable.DEFAULT_PAGE_NUMBER)
        {
            if (category is null) { return RedirectToAction("Index", "Home"); }

            ProductListReadDto data = await _productService.GetCategoryProductData(category, page, ConstantVariable.DEFAULT_SIZE_PER_PAGE);

            if (data is null)
            {
                return RedirectToAction("Index", new { category = "TatCaSanPham" });
            }

            var vm = new ProductListViewModel
            {
                Products = data.Products,
                Pagination = new PaginationViewModel
                {
                    TotalPage = data.TotalPage,
                    CurrentPage = data.CurrentPage,
                    Category = category,
                    Controller = "Product",
                    Action = "Index"
                }
            };

            ViewData["CurrentCategory"] = category;
            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0) 
                return RedirectToAction("Index", new { category = "TatCaSanPham" });

            ProductDetailReadDto productDetail = await _productService.GetProductDetailData(id);

            if (productDetail is null)
            {
                return RedirectToAction("Index", "Error");
            }

            var vm = new ProductDetailViewModel()
            {
                Product = productDetail,
            };

            ViewData["Size"] = ConstantVariable.NUMBER_OF_RELATIVE_PRODUCTS;
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Rating(ProductRatingWriteDto data)
        {
            if (data is null || data.ProductId <= 0 || data.Stars <= 0)
                return BadRequest();

            var result = await _productService.ProductRating(data);
            if (result == PostResponse.UNAUTHORIZED)
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Unauthorized();
            }
                
            return result == PostResponse.OK ? Ok() : BadRequest();
        }
    }
}
