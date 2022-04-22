using System.Threading.Tasks;
using CustomerSite.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSite.ViewComponents
{
    public class RelativeProductsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        private const int DEFAULT_RELATIVE_SIZE = 4;

        public RelativeProductsViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id, int size = DEFAULT_RELATIVE_SIZE)
        {
            var products = await _productService.GetRelativeProductData(id, size);
            return View(products);
        }
    }
}